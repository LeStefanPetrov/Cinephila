using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Cinephila.Domain.BackgroundServices;
using Cinephila.Domain.DTOs.FetchDataDTOs;
using Cinephila.Domain.Repositories;
using Cinephila.Domain.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cinephila.Services.BackgroundServices
{
    public class MovieFetcherService : BaseFetcherService<MovieDto>, IMovieFetcherService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;
        private readonly JsonSerializerOptions _options;
        private readonly IProductionsRepository _productionsRepository;
        private readonly ILogger<MovieFetcherService> _logger;

        public MovieFetcherService(
            HttpClient httpClient,
            IOptions<ApiSettings> apiSettings,
            JsonSerializerOptions options,
            IProductionsRepository productionsRepository,
            ILogger<MovieFetcherService> logger)
            : base(httpClient, apiSettings, options, logger)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
            _options = options;
            _productionsRepository = productionsRepository;
            _logger = logger;
        }

        public async Task ProcessMovieListAsync()
        {
            if (await _productionsRepository.AnyAsync())
            {
                _logger.LogInformation("Productions table contains data. Skipping fetch operation.");
                return;
            }

            _logger.LogInformation("Starting fetching movies operation.");

            await ProcessFileAsync(FetchInfoAsync, _productionsRepository.BatchInsertMovieProductionsAsync,  _apiSettings.FetchMoviesUrl);

            _logger.LogInformation("Finished fetching movies operation.");
        }

        public override async Task<MovieDto> FetchInfoAsync(int recordId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"movie/{recordId}?api_key={_apiSettings.Key}");
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                var movieDto = JsonSerializer.Deserialize<MovieDto>(content, _options);
                return movieDto;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while fetching movies from API.");
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error while deserializing movie details.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured:");
            }

            return null;
        }
    }
}