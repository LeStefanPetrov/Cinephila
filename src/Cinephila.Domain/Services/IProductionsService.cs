using Cinephila.Domain.DTOs.ProductionDTOs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinephila.Domain.Services
{
    public interface IProductionsService
    {
        Task<int> CreateAsync(Production dto);

        Task UpdateAsync(Production dto, int id);

        Task DeleteAsync(int id);

        Task<bool> CheckIfExistAsync(int id);

        Task<IEnumerable<Production>> GetPaginatedAsync(int page, int size);
    }
}
