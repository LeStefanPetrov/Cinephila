using Cinephila.Domain.BackgroundServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cinephila.Services.BackgroundServices
{
    public class DataFetcherService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DataFetcherService> _logger;
        public DataFetcherService(
            IServiceProvider serviceProvider,
            ILogger<DataFetcherService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _ = Task.Run(async () =>
            {
                try
                {

                    using var scope = _serviceProvider.CreateScope();

                    var genreFetcherService = scope.ServiceProvider.GetRequiredService<IGenreFetcherService>();
                    var movieFetcherService = scope.ServiceProvider.GetRequiredService<IMovieFetcherService>();
                    var personFetcherService = scope.ServiceProvider.GetRequiredService<IPersonFetcherService>();

                    _logger.LogInformation("Start fetching data from API.");

                    // Fetch genre, movie and person data sequentially
                    await genreFetcherService.FetchGenresAsync();
                    await movieFetcherService.ProcessMovieListAsync();
                    await personFetcherService.ProcessPersonListAsync();

                    _logger.LogInformation("Fetching data complete.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while fetching data.");
                }
            }, stoppingToken);

        }
    }
}
