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
    public class PersonFetcherService : BaseFetcherService<PersonDto>, IPersonFetcherService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;
        private readonly JsonSerializerOptions _options;
        private readonly IParticipantsRepository _participantsRepository;
        private readonly ILogger<PersonFetcherService> _logger;

        public PersonFetcherService(
            HttpClient httpClient,
            IOptions<ApiSettings> apiSettings,
            JsonSerializerOptions options,
            IParticipantsRepository participantsRepository,
            ILogger<PersonFetcherService> logger)
            : base(httpClient, apiSettings, options, logger)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
            _options = options;
            _participantsRepository = participantsRepository;
            _logger = logger;
        }

        public async Task ProcessPersonListAsync()
        {
            if (await _participantsRepository.AnyAsync())
            {
                _logger.LogInformation("Participants table contains data. Skipping fetch operation.");
                return;
            }

            _logger.LogInformation("Starting fetching people operation.");

            await ProcessFileAsync(FetchInfoAsync, _participantsRepository.BatchInsertParticipantsAsync, _apiSettings.FetchPeopleUrl);

            _logger.LogInformation("Finished fetching people operation.");
        }

        public override async Task<PersonDto> FetchInfoAsync(int recordId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"person/{recordId}?api_key={_apiSettings.Key}&append_to_response=movie_credits,images");
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                var personDto = JsonSerializer.Deserialize<PersonDto>(content, _options);
                return personDto;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while fetching people from API.");
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error while deserializing person details.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured:");
            }

            return null;
        }
    }
}
