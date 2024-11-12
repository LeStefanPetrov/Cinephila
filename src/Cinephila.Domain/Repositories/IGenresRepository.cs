using Cinephila.Domain.DTOs.FetchDataDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinephila.Domain.Repositories
{
    public interface IGenresRepository
    {
        Task SeedGenresAsync(IEnumerable<GenreDto> genreDtos);
    }
}
