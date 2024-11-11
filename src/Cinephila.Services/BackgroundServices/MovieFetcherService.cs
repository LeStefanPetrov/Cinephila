using System.IO;
using System.Text.Json;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Cinephila.Domain.Settings;
using Microsoft.Extensions.Options;
using Cinephila.Domain.DTOs.ApiDTOs;
using Cinephila.Domain.BackgroundServices;
using Cinephila.Domain.Helpers;

namespace Cinephila.Services.BackgroundServices
{
    public class MovieFetcherService : IMovieFetcherService
    {
        private readonly ApiSettings _apiSettings;
        private readonly JsonSerializerOptions _options;

        public MovieFetcherService(
            IOptions<ApiSettings> apiSettings,
            JsonSerializerOptions options)
        {
            _apiSettings = apiSettings.Value;
            _options = options;
        }

        public async Task ProcessMovieListAsync()
        {
            var projectRootDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.FullName;
            string relativeFilePath = Path.Combine(projectRootDirectory, @"dataseed\movie_ids_11_11_2024.json");

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
                        TmdbRecordImport movie = JsonSerializer.Deserialize<TmdbRecordImport>(line, _options);

                        if (movie != null && movie.Popularity > 10)
                        {
                            FetchMovieInfoAsync(movie.Id);
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

        private void FetchMovieInfoAsync(int movieId)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(_apiSettings.Url);
                    HttpResponseMessage response = client.GetAsync($"movie/{movieId}?api_key={_apiSettings.Key}&append_to_response=credits").Result;
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