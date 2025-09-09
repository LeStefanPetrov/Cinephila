using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cinephila.DataAccess.Entities;
using Cinephila.Domain.DTOs.ReviewDTOs;
using Cinephila.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cinephila.DataAccess.Repositories
{
    public class ReviewsRepository : IReviewsRepository
    {
        private readonly CinephilaDbContext _context;
        private readonly IMapper _mapper;

        public ReviewsRepository(CinephilaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(Review dto)
        {
            var entity = _mapper.Map<ReviewProductionEntity>(dto);
            _context.ReviewsProductions.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return entity.ID;
        }

        public async Task UpdateAsync(Review dto, int id)
        {
            var entity = await _context.ReviewsProductions.FirstOrDefaultAsync(x => x.ID == id).ConfigureAwait(false);
            _mapper.Map(dto, entity);
            _context.ReviewsProductions.Update(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ReviewsProductions.FirstOrDefaultAsync(x => x.ID == id).ConfigureAwait(false);
            _context.ReviewsProductions.Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<List<Review>> GetPaginatedAsync(int currentPage, int pageSize)
        {
            var entities = await _context.ReviewsProductions.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync().ConfigureAwait(false);

            return _mapper.Map<List<Review>>(entities);
        }

        public async Task<bool> CheckIfExistAsync(int id)
        {
            return await _context.ReviewsProductions.AnyAsync(x => x.ID == id).ConfigureAwait(false);
        }
    }
}
