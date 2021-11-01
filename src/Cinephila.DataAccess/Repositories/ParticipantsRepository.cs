﻿using AutoMapper;
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

        public async Task<int> CreateAsync(ParticipantDto dto)
        {
            var entity = _mapper.Map<Participant>(dto);
            _context.Participants.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return entity.ID;
        }
        public async Task UpdateAsync(ParticipantDto dto, int id)
        {
            var entity = _context.Participants.FirstOrDefault(x => x.ID == id);
            _mapper.Map(dto, entity);
            _context.Participants.Update(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _context.Participants.FirstOrDefault(x => x.ID == id);
            _context.Participants.Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<List<ParticipantDto>> GetPaginatedAsync(int currentPage, int pageSize)
        {
            var entities = await _context.Participants.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync().ConfigureAwait(false);

            return _mapper.Map<List<ParticipantDto>>(entities);
        }

        public async Task<bool> CheckIfExistAsync(int id)
        {
            return await _context.Participants.AnyAsync(x => x.ID == id).ConfigureAwait(false);
        }
    }
}