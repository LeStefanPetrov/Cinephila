using System.Text.Json;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Cinephila.Domain.Settings;
using Microsoft.Extensions.Options;
using Cinephila.Domain.BackgroundServices;

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

        public Task FetchMovieInfoAsync(int recordId)
        {
            throw new NotImplementedException();
        }
    }
}