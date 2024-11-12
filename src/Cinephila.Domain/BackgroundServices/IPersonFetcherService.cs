using System.Threading.Tasks;

namespace Cinephila.Domain.BackgroundServices
{
    public interface IPersonFetcherService
    {
        Task ProcessPersonListAsync();
        Task FetchPersonInfoAsync(int recordId);
    }
}
