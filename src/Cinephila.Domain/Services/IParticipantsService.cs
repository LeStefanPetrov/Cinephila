using Cinephila.Domain.DTOs.ParticipantDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.Domain.Services
{
    public interface IParticipantsService
    {
        Task<int> CreateAsync(Participant dto);

        Task UpdateAsync(Participant dto, int id);

        Task DeleteAsync(int id);

        Task<List<Participant>> GetPaginatedAsync(int currentPage, int pageSize);

        Task<bool> CheckIfExistAsync(int id);
    }
}
