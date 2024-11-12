using System.Text.Json;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Cinephila.Domain.Settings;
using Microsoft.Extensions.Options;
using Cinephila.Domain.BackgroundServices;
using Cinephila.Domain.DTOs.FetchDataDTOs;
using System.Collections.Generic;

namespace Cinephila.Services.BackgroundServices
{
    public class MovieFetcherService : BaseFetcherService, IMovieFetcherService
    {
        private readonly HttpClient _httpClient; 
        private readonly ApiSettings _apiSettings;
        private readonly JsonSerializerOptions _options;
        public MovieFetcherService(
            HttpClient httpClient,
            IOptions<ApiSettings> apiSettings,
            JsonSerializerOptions options) : base(httpClient, apiSettings, options)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
            _options = options;
        }

        public async Task ProcessMovieListAsync()
        {
            await ProcessFileAsync(FetchMovieInfoAsync, _apiSettings.FetchMoviesUrl);
        }

        public async Task<MovieDto> FetchMovieInfoAsync(int recordId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"genre/movie/list?api_key={_apiSettings.Key}");
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            var personDto = JsonSerializer.Deserialize<List<GenreDto>>(content, _options);

            return new MovieDto();
        }
    }
}