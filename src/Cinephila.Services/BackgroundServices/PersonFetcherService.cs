using Cinephila.Domain.DTOs.ApiDTOs;
using Cinephila.Domain.Settings;
using Microsoft.Extensions.Options;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System;
using System.Threading.Tasks;
using Cinephila.Domain.BackgroundServices;
using Cinephila.Domain.Helpers;

namespace Cinephila.Services.BackgroundServices
{
    public class PersonFetcherService : IPersonFetcherService
    {
        private readonly ApiSettings _apiSettings;
        private readonly JsonSerializerOptions _options;

        public PersonFetcherService(
            IOptions<ApiSettings> apiSettings,
            JsonSerializerOptions options)
        {
            _apiSettings = apiSettings.Value;
            _options = options;
        }

        public async Task ProcessPersonListAsync()
        {
            var projectRootDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.FullName;
            string relativeFilePath = Path.Combine(projectRootDirectory, @"dataseed\person_ids_11_11_2024.json");

            if (!File.Exists(relativeFilePath))
            {
                // Log error
                return;
            }

            using (var streamReader = new StreamReader(relativeFilePath))
            {
                string line;

                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    try
                    {
                        TmdbRecordImport person = JsonSerializer.Deserialize<TmdbRecordImport>(line, _options);

                        if (person != null && person.Popularity > 10)
                        {
                            FetchPersonInfoAsync(person.Id);
                        }
                        else
                        {
                            // Log error
                        }
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Error deserializing line: {ex.Message}");
                    }
                }
            }
        }

        private void FetchPersonInfoAsync(int personId)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(_apiSettings.Url);
                    HttpResponseMessage response = client.GetAsync($"person/{personId}?api_key={_apiSettings.Key}").Result;
                    var message = response.EnsureSuccessStatusCode();

                    string popularMoviesResult = response.Content.ReadAsStringAsync().Result;
                    var moviesDto = JsonSerializer.Deserialize<PopularMoviesDto>(popularMoviesResult).Movies;
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
