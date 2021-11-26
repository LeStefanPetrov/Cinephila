using AutoMapper;
using Cinephila.DataAccess.Entities;
using Cinephila.Domain.DTOs.ProductionDTOs;
using Cinephila.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cinephila.DataAccess.Repositories
{
    public class ProductionsRepository : IProductionsRepository
    {
        private readonly CinephilaDbContext _context;
        private readonly IMapper _mapper;

        public ProductionsRepository(CinephilaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(Production dto)
        {
                var productionEntity = _mapper.Map<ProductionEntity>(dto);
                _context.Productions.Add(productionEntity);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return productionEntity.ID;
        }

        public async Task UpdateAsync(Production dto, int id)
        {
            if (dto is Movie)
            {
                var movie = await _context.Movies.Where(x => x.ProductionID == id).FirstOrDefaultAsync().ConfigureAwait(false);
                _mapper.Map(dto,movie);
                _context.Movies.Update(movie);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                var show = await _context.TVShows.Where(x => x.ProductionID == id).FirstOrDefaultAsync().ConfigureAwait(false);
                _mapper.Map(dto, show);
                _context.TVShows.Update(show);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Productions.FirstOrDefaultAsync(x => x.ID == id).ConfigureAwait(false);
            _context.Productions.Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<bool> CheckIfExistAsync(int id)
        {
            return await _context.Productions.AnyAsync(x => x.ID == id).ConfigureAwait(false);
        }
    }
}
