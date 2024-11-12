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

        public DataFetcherService(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ = Task.Run(async () =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    try
                    {
                        var personFetcherService = scope.ServiceProvider.GetRequiredService<IPersonFetcherService>();
                        var movieFetcherService = scope.ServiceProvider.GetRequiredService<IMovieFetcherService>();
                        var genreFetcherService = scope.ServiceProvider.GetRequiredService<IGenreFetcherService>();

                        // Fetch person and movie data sequentially
                        await genreFetcherService.FetchGenresAsync();
                        await personFetcherService.ProcessPersonListAsync();
                        await movieFetcherService.ProcessMovieListAsync();
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
