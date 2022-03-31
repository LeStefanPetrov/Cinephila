using AutoMapper;
using Cinephila.DataAccess.Entities;
using Cinephila.Domain.DTOs.ParticipantDTOs;
using Cinephila.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.DataAccess.Repositories
{
    public class ParticipantsRepository : IParticipantsRepository
    {
        private readonly CinephilaDbContext _context;
        private readonly IMapper _mapper;

        public ParticipantsRepository(CinephilaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(Participant dto)
        {
            var entity = _mapper.Map<ParticipantEntity>(dto);
            _context.Participants.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return entity.ID;
        }

        public async Task UpdateAsync(Participant dto, int id)
        {
            var entity = await _context.Participants.FirstOrDefaultAsync(x => x.ID == id).ConfigureAwait(false);
            _mapper.Map(dto, entity);
            _context.Participants.Update(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Participants.FirstOrDefaultAsync(x => x.ID == id).ConfigureAwait(false);
            _context.Participants.Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<List<Participant>> GetPaginatedAsync(int currentPage, int pageSize)
        {
            var entities = await _context.Participants.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync().ConfigureAwait(false);

            return _mapper.Map<List<Participant>>(entities);
        }

        public async Task<bool> CheckIfExistAsync(int id)
        {
            return await _context.Participants.AnyAsync(x => x.ID == id).ConfigureAwait(false);
        }
    }
}
