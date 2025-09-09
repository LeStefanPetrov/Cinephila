using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Cinephila.Domain.Helpers;
using Cinephila.Domain.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cinephila.Services.BackgroundServices
{
    public abstract class BaseFetcherService<T>
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;
        private readonly JsonSerializerOptions _options;
        private readonly ILogger<BaseFetcherService<T>> _logger;

        private const int ConcurrentOperationsLimit = 50;
        private const int RecordsBatchLimit = 200;

        protected BaseFetcherService(
            HttpClient httpClient,
            IOptions<ApiSettings> apiSettings,
            JsonSerializerOptions options,
            ILogger<BaseFetcherService<T>> logger)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
            _options = options;
            _logger = logger;
        }

        protected async Task ProcessFileAsync<T>(Func<int, Task<T>> fetchDetailsAsync, Func<IEnumerable<T>, Task> saveRecordsBatchAsync, string fetchUrl)
        {
            string tempGzFilePath = Path.Combine(Path.GetTempPath(), "records.json.gz");
            string tempJsonFilePath = Path.Combine(Path.GetTempPath(), "records.json");


            await DownloadAndDecompressDataAsync(fetchUrl, tempGzFilePath, tempJsonFilePath);

            if (!File.Exists(tempJsonFilePath))
            {
                _logger.LogInformation("File with the name '{tempJsonFilePath}' does not exist.", tempJsonFilePath);
                return;
            }

            try
            {
                var fetchRecordDetailsTasks = new List<Task<T>>();
                var fetchedRecordsBatch = new List<T>();

                using (var streamReader = new StreamReader(tempJsonFilePath))
                {
                    string line;

                    while ((line = await streamReader.ReadLineAsync()) != null)
                    {
                        var record = JsonSerializer.Deserialize<TmdbRecordImport>(line, _options);

                        if (record == null || record.Popularity <= _apiSettings.MinimumPopularity)
                            continue;

                        fetchRecordDetailsTasks.Add(fetchDetailsAsync(record.Id));

                        if (fetchRecordDetailsTasks.Count >= ConcurrentOperationsLimit)
                        {
                            var results = await Task.WhenAll(fetchRecordDetailsTasks);
                            fetchRecordDetailsTasks.Clear();

                            fetchedRecordsBatch.AddRange(results.Where(r => r != null));

                            if (fetchedRecordsBatch.Count >= RecordsBatchLimit)
                            {
                                await saveRecordsBatchAsync(fetchedRecordsBatch);
                                fetchedRecordsBatch.Clear();
                            }
                        }
                    }

                    // Process any remaining tasks in the final batch
                    if (fetchRecordDetailsTasks.Any())
                    {
                        var results = await Task.WhenAll(fetchRecordDetailsTasks);
                        fetchedRecordsBatch.AddRange(results.Where(r => r != null));
                    }

                    // Final save for any remaining buffered data
                    if (fetchedRecordsBatch.Any())
                    {
                        await saveRecordsBatchAsync(fetchedRecordsBatch);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing file.");
            }
            finally
            {
                CleanUp(tempGzFilePath, tempJsonFilePath);
            }
        }

        private async Task DownloadAndDecompressDataAsync(string fetchUrl, string tempGzFilePath, string tempJsonFilePath)
        {
            // Download the .gz file
            using (var response = await _httpClient.GetAsync(GenerateFetchUrl(fetchUrl), HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                await using (var fs = new FileStream(tempGzFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await response.Content.CopyToAsync(fs);
                }
            }

            // Decompress the .gz file
            using (FileStream originalFileStream = new FileStream(tempGzFilePath, FileMode.Open))
            using (FileStream decompressedFileStream = new FileStream(tempJsonFilePath, FileMode.Create))
            using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
            {
                await decompressionStream.CopyToAsync(decompressedFileStream);
            }
        }

        private string GenerateFetchUrl(string urlFormat)
        {
            return urlFormat
                .Replace("{MM}", DateTime.Today.Month.ToString("D2"))
                .Replace("{DD}", DateTime.Today.Day.ToString("D2"))
                .Replace("{YYYY}", DateTime.Today.Year.ToString());
        }

        private void CleanUp(string tempGzFilePath, string tempJsonFilePath)
        {
            File.Delete(tempGzFilePath);
            File.Delete(tempJsonFilePath);
        }

        public abstract Task<T> FetchInfoAsync(int recordId);
    }
}
