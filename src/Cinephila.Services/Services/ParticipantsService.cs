using Cinephila.Domain.DTOs.ParticipantsDTOs;
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
        private readonly IParticipantsRepository _repository;

        public ParticipantsService(IParticipantsRepository repository)
        {
            _repository = repository;
        }

        public Task<int> CreateAsync(ParticipantDto dto)
        {
            return _repository.CreateAsync(dto);
        }

        public Task UpdateAsync(ParticipantDto dto, int id)
        {
            return _repository.UpdateAsync(dto, id);
        }

        public Task DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public Task<List<ParticipantDto>> GetPaginatedAsync(int currentPage, int pageSize)
        {
            return _repository.GetPaginatedAsync(currentPage, pageSize);
        }

        public Task<bool> CheckIfExistAsync(int id)
        {
            return _repository.CheckIfExistAsync(id);
        }
    }
}
