using Cinephila.Domain.DTOs.FetchDataDTOs;
using System.Threading.Tasks;

namespace Cinephila.Domain.BackgroundServices
{
    public interface IPersonFetcherService
    {
        Task ProcessPersonListAsync();
        Task<PersonDto> FetchPersonInfoAsync(int recordId);
    }
}
