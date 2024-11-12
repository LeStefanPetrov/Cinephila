using Cinephila.Domain.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Cinephila.Domain.BackgroundServices;
using Cinephila.Domain.DTOs.FetchDataDTOs;

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

        public async Task<PersonDto> FetchPersonInfoAsync(int recordId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"person/{recordId}?api_key={_apiSettings.Key}");
            response.EnsureSuccessStatusCode();

            string content  = await response.Content.ReadAsStringAsync();
            var personDto = JsonSerializer.Deserialize<PersonDto>(content, _options);

            return personDto;
        }
    }
}
