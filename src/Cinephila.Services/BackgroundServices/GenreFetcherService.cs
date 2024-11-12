using Cinephila.Domain.BackgroundServices;
using Cinephila.Domain.DTOs.FetchDataDTOs;
using Cinephila.Domain.Repositories;
using Cinephila.Domain.Settings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
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

        public GenreFetcherService(
            HttpClient httpClient,
            IOptions<ApiSettings> apiSettings,
            JsonSerializerOptions options,
            IGenresRepository genresRepository)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
            _options = options;
            _genresRepository = genresRepository;
        }

        public async Task FetchGenresAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"genre/movie/list?api_key={_apiSettings.Key}");
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            var genresResponse = JsonSerializer.Deserialize<GenresResponse>(content, _options);

            await _genresRepository.BatchInsertGenresAsync(genresResponse.Genres);
        }
    }
}
