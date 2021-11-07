using Cinephila.Domain.DTOs.ParticipantDTOs;
using Cinephila.Domain.Repositories;
using Cinephila.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.Services.Services
{
    public class ParticipantsService : IParticipantsService
    {
        private readonly IParticipantsRepository _participantsRepository;

        public ParticipantsService(IParticipantsRepository participantsRepository)
        {
            _participantsRepository = participantsRepository;
        }

        public Task<int> CreateAsync(Participant dto)
        {
            return _participantsRepository.CreateAsync(dto);
        }

        public Task UpdateAsync(Participant dto, int id)
        {
            return _participantsRepository.UpdateAsync(dto, id);
        }

        public Task DeleteAsync(int id)
        {
            return _participantsRepository.DeleteAsync(id);
        }

        public Task<List<Participant>> GetPaginatedAsync(int currentPage, int pageSize)
        {
            return _participantsRepository.GetPaginatedAsync(currentPage, pageSize);
        }

        public Task<bool> CheckIfExistAsync(int id)
        {
            return _participantsRepository.CheckIfExistAsync(id);
        }
    }
}
