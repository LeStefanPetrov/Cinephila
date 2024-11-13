using Cinephila.Domain.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Cinephila.Domain.BackgroundServices;
using Cinephila.Domain.DTOs.FetchDataDTOs;
using Cinephila.Domain.Repositories;
using System;

namespace Cinephila.Services.BackgroundServices
{
    public class PersonFetcherService : BaseFetcherService, IPersonFetcherService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;
        private readonly JsonSerializerOptions _options;
        private readonly IParticipantsRepository _participantsRepository;

        public PersonFetcherService(
            HttpClient httpClient,
            IOptions<ApiSettings> apiSettings,
            JsonSerializerOptions options,
            IParticipantsRepository participantsRepository) : base(httpClient, apiSettings, options)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
            _options = options;
            _participantsRepository = participantsRepository;
        }

        public async Task ProcessPersonListAsync()
        {
            await ProcessFileAsync(FetchPersonInfoAsync, _participantsRepository.BatchInsertParticipantsAsync, _apiSettings.FetchPeopleUrl);
        }

        public async Task<PersonDto> FetchPersonInfoAsync(int recordId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"person/{recordId}?api_key={_apiSettings.Key}&append_to_response=movie_credits,images");
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                var personDto = JsonSerializer.Deserialize<PersonDto>(content, _options);
                return personDto;
            }
            catch (Exception e)
            {
                // Log error
            }

            return null;
        }
    }
}
