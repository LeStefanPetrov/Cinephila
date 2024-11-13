using System.Text.Json;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Cinephila.Domain.Settings;
using Microsoft.Extensions.Options;
using Cinephila.Domain.BackgroundServices;
using Cinephila.Domain.DTOs.FetchDataDTOs;
using Cinephila.Domain.Repositories;

namespace Cinephila.Services.BackgroundServices
{
    public class MovieFetcherService : BaseFetcherService, IMovieFetcherService
    {
        private readonly HttpClient _httpClient; 
        private readonly ApiSettings _apiSettings;
        private readonly JsonSerializerOptions _options;
        private readonly IProductionsRepository _productionsRepository;

        public MovieFetcherService(
            HttpClient httpClient,
            IOptions<ApiSettings> apiSettings,
            JsonSerializerOptions options,
            IProductionsRepository productionsRepository) : base(httpClient, apiSettings, options)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
            _options = options;
            _productionsRepository = productionsRepository;
        }

        public async Task ProcessMovieListAsync()
        {
            await ProcessFileAsync(FetchMovieInfoAsync, _productionsRepository.BatchInsertMovieProductionsAsync,  _apiSettings.FetchMoviesUrl);
        }

        public async Task<MovieDto> FetchMovieInfoAsync(int recordId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"movie/{recordId}?api_key={_apiSettings.Key}");
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
            
                var movieDto = JsonSerializer.Deserialize<MovieDto>(content, _options);
            }
            catch (Exception e)
            {
                return null;
            }

            return null;
        }
    }
}