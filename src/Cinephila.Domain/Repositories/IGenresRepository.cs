using System.Collections.Generic;
using System.Threading.Tasks;
using Cinephila.Domain.DTOs.FetchDataDTOs;

namespace Cinephila.Domain.Repositories
{
    public interface IGenresRepository
    {
        Task BatchInsertGenresAsync(IEnumerable<GenreDto> genreDtos);

        Task<bool> AnyAsync();
    }
}
