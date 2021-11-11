using Cinephila.Domain.DTOs.ProductionDTOs;
using System.Threading.Tasks;

namespace Cinephila.Domain.Services
{
    public interface IProductionsService
    {
        Task<int> CreateAsync(Production dto);

        Task UpdateAsync(Production dto, int id);

        Task DeleteAsync(int id);

        Task<bool> CheckIfExistAsync(int id);
    }
}
