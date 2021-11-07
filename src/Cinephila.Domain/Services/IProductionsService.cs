using Cinephila.Domain.DTOs.ProductionDTOs;
using System.Threading.Tasks;

namespace Cinephila.Domain.Services
{
    public interface IProductionsService
    {
        Task<int> CreateAsync(Production dto);

        Task DeleteAsync(int id);
    }
}
