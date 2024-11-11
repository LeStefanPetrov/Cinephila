using Cinephila.Domain.BackgroundServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cinephila.Services.BackgroundServices
{
    public class DataFetcherService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPersonFetcherService _personFetcherService;
        private readonly IMovieFetcherService _movieFetcherService;

        public DataFetcherService(
            IServiceProvider serviceProvider, 
            IPersonFetcherService personFetcherService, 
            IMovieFetcherService movieFetcherService)
        {
            _serviceProvider = serviceProvider;
            _personFetcherService = personFetcherService;
            _movieFetcherService = movieFetcherService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ = Task.Run(async () =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    try
                    {
                        // Fetch person and movie data sequentially
                        await _personFetcherService.ProcessPersonListAsync();
                        await _movieFetcherService.ProcessMovieListAsync();
                    }
                    catch (Exception ex)
                    {
                        // Log exception (handle errors as needed)
                        Console.WriteLine($"Error executing sequential tasks: {ex.Message}");
                    }
                }
            }, stoppingToken);
        }
    }
}
