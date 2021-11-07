using Cinephila.Domain.DTOs.ProductionDTOs;
using System.Threading.Tasks;

namespace Cinephila.Domain.Repositories
{
    public interface IProductionsRepository
    {
        Task<int> CreateAsync(Production dto);

        Task DeleteAsync(int id);
    }
}
