using Cinephila.Domain.DTOs.ParticipantsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.Domain.Repositories
{
    public interface IParticipantsRepository
    {
        Task<int> CreateAsync(ParticipantDto dto);

        Task UpdateAsync(ParticipantDto dto, int id);

        Task DeleteAsync(int id);

        Task<List<ParticipantDto>> GetPaginatedAsync(int currentPage, int pageSize);

        Task<bool> CheckIfExistAsync(int id);
    }
}
