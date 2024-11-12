using System.Threading.Tasks;

namespace Cinephila.Domain.BackgroundServices
{
    public interface IGenreFetcherService
    {
        Task FetchGenresAsync();
    }
}
