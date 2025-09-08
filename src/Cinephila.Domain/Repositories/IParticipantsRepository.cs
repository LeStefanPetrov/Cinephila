using Cinephila.Domain.DTOs.FetchDataDTOs;
using Cinephila.Domain.DTOs.ParticipantDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinephila.Domain.Repositories
{
    public interface IParticipantsRepository
    {
        Task<int> CreateAsync(Participant dto);

        Task UpdateAsync(Participant dto, int id);

        Task DeleteAsync(int id);

        Task<List<Participant>> GetPaginatedAsync(int currentPage, int pageSize);

        Task<bool> CheckIfExistAsync(int id);

        Task BatchInsertParticipantsAsync(IEnumerable<PersonDto> participantDtos);

        Task<bool> AnyAsync();
    }
}
