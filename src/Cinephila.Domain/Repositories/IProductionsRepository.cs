using Cinephila.Domain.DTOs.FetchDataDTOs;
using Cinephila.Domain.DTOs.ProductionDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinephila.Domain.Repositories
{
    public interface IProductionsRepository
    {
        Task<int> CreateAsync(Production dto);

        Task UpdateAsync(Production dto, int id);

        Task DeleteAsync(int id);

        Task<bool> CheckIfExistAsync(int id);

        Task<IEnumerable<Production>> GetPaginatedAsync(int page, int size);

        Task BatchInsertMovieProductionsAsync(IEnumerable<MovieDto> movieDtos);
    }
}
