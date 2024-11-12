using Cinephila.Domain.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System;
using System.Threading.Tasks;
using Cinephila.Domain.BackgroundServices;

namespace Cinephila.Services.BackgroundServices
{
    public class PersonFetcherService : BaseFetcherService, IPersonFetcherService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;
        private readonly JsonSerializerOptions _options;

        public PersonFetcherService(
            HttpClient httpClient,
            IOptions<ApiSettings> apiSettings,
            JsonSerializerOptions options) : base(httpClient, apiSettings, options)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
            _options = options;
        }

        public async Task ProcessPersonListAsync()
        {
            await ProcessFileAsync(FetchPersonInfoAsync, _apiSettings.FetchPeopleUrl);
        }

        public Task FetchPersonInfoAsync(int recordId)
        {
            throw new NotImplementedException();
        }
    }
}
