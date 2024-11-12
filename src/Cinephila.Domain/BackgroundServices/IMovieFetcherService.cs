using System.Threading.Tasks;

namespace Cinephila.Domain.BackgroundServices
{
    public interface IMovieFetcherService
    {
        Task ProcessMovieListAsync();
        Task FetchMovieInfoAsync(int recordId);
    }
}
