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
                        var genreFetcherService = scope.ServiceProvider.GetRequiredService<IGenreFetcherService>();
                        var movieFetcherService = scope.ServiceProvider.GetRequiredService<IMovieFetcherService>();
                        var personFetcherService = scope.ServiceProvider.GetRequiredService<IPersonFetcherService>();

                        // Fetch genre, movie and person data sequentially
                        // await genreFetcherService.FetchGenresAsync();
                        await movieFetcherService.ProcessMovieListAsync();
                        await personFetcherService.ProcessPersonListAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error executing sequential tasks: {ex.Message}");
                    }
                }
            }, stoppingToken);
        }
    }
}
