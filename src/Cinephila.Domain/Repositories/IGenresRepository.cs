using Cinephila.Domain.DTOs.FetchDataDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinephila.Domain.Repositories
{
    public interface IGenresRepository
    {
        Task BatchInsertGenresAsync(IEnumerable<GenreDto> genreDtos);

        Task<bool> AnyAsync();
    }
}
