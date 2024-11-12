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
        private const int BatchSize = 1;
        private const int BufferSize = 200;

        protected BaseFetcherService(
            HttpClient httpClient,
            IOptions<ApiSettings> apiSettings,
            JsonSerializerOptions options)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
            _options = options;
        }

        protected async Task ProcessFileAsync<T>(Func<int, Task<T>> fetchDetailsAsync, string fetchUrl)
        {
            string tempGzFilePath = Path.Combine(Path.GetTempPath(), "records.json.gz");
            string tempJsonFilePath = Path.Combine(Path.GetTempPath(), "records.json");

           //await DownloadAndDecompressDataAsync(fetchUrl, tempGzFilePath, tempJsonFilePath);

            if (!File.Exists(tempJsonFilePath))
            {
                // Log error
                return;
            }

            var apiTasks = new List<Task<T>>();
            var dbBuffer = new List<T>();

            using (var streamReader = new StreamReader(tempJsonFilePath))
            {
                string line;

                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    try
                    {
                        var record = JsonSerializer.Deserialize<TmdbRecordImport>(line, _options);

                        if (record == null || record.Popularity <= 5) 
                            continue;

                        apiTasks.Add(fetchDetailsAsync(record.Id));

                        if (apiTasks.Count >= BatchSize)
                        {
                            var results = await Task.WhenAll(apiTasks); // Wait for all API tasks to complete
                            apiTasks.Clear(); // Clear tasks for the next batch

                            // Add results to the buffer for bulk saving
                            dbBuffer.AddRange(results.Where(r => r != null));

                            // Save to DB if buffer size is reached
                            if (dbBuffer.Count >= BufferSize)
                            {
                                // Save records
                                dbBuffer.Clear();
                            }
                        }
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Error deserializing line: {ex.Message}");
                    }
                }
            }

            // Clean up temporary files
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
