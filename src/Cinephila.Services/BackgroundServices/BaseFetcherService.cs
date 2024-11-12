using Cinephila.Domain.Helpers;
using Cinephila.Domain.Settings;
using System.IO.Compression;
using System.IO;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Cinephila.Services.BackgroundServices
{
    public abstract class BaseFetcherService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;
        private readonly JsonSerializerOptions _options;
        private const int ConcurrentOperationsLimit = 50;
        private const int RecordsBatchLimit = 200;

        protected BaseFetcherService(
            HttpClient httpClient,
            IOptions<ApiSettings> apiSettings,
            JsonSerializerOptions options)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
            _options = options;
        }

        protected async Task ProcessFileAsync<T>(Func<int, Task<T>> fetchDetailsAsync, Func<IEnumerable<T>, Task> saveRecordsBatchAsync, string fetchUrl)
        {
            string tempGzFilePath = Path.Combine(Path.GetTempPath(), "records.json.gz");
            string tempJsonFilePath = Path.Combine(Path.GetTempPath(), "records.json");

            await DownloadAndDecompressDataAsync(fetchUrl, tempGzFilePath, tempJsonFilePath);

            if (!File.Exists(tempJsonFilePath))
            {
                // Log error
                return;
            }

            var fetchRecordDetailsTasks = new List<Task<T>>();
            var fetchedRecordsBatch = new List<T>();

            using (var streamReader = new StreamReader(tempJsonFilePath))
            {
                string line;

                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    var record = JsonSerializer.Deserialize<TmdbRecordImport>(line, _options);

                    if (record == null || record.Popularity <= 20) 
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

            File.Delete(tempGzFilePath);
            File.Delete(tempJsonFilePath);
        }

        protected async Task DownloadAndDecompressDataAsync(string fetchUrl, string tempGzFilePath, string tempJsonFilePath)
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

        protected string GenerateFetchUrl(string urlFormat)
        {
            return urlFormat
                .Replace("{MM}", DateTime.Today.Month.ToString("D2"))
                .Replace("{DD}", DateTime.Today.Day.ToString("D2"))
                .Replace("{YYYY}", DateTime.Today.Year.ToString());
        }
    }
}
