using Cinephila.Domain.BackgroundServices;
using Cinephila.Domain.DTOs.FetchDataDTOs;
using Cinephila.Domain.Repositories;
using Cinephila.Domain.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cinephila.Services.BackgroundServices
{
    public class GenreFetcherService : IGenreFetcherService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;
        private readonly JsonSerializerOptions _options;
        private readonly IGenresRepository _genresRepository;
        private readonly ILogger<GenreFetcherService> _logger;

        public GenreFetcherService(
            HttpClient httpClient,
            IOptions<ApiSettings> apiSettings,
            JsonSerializerOptions options,
            IGenresRepository genresRepository,
            ILogger<GenreFetcherService> logger)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
            _options = options;
            _genresRepository = genresRepository;
            _logger = logger;
        }

        public async Task FetchGenresAsync()
        {
            if (await _genresRepository.AnyAsync())
            {
                _logger.LogInformation("Genres table contains data. Skipping fetch operation.");
                return;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"genre/movie/list?api_key={_apiSettings.Key}");
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                var genresResponse = JsonSerializer.Deserialize<GenresResponse>(content, _options);

                await _genresRepository.BatchInsertGenresAsync(genresResponse.Genres);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while fetching genres from API.");
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error while deserializing genres.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured:");
            }
        }
    }
}
