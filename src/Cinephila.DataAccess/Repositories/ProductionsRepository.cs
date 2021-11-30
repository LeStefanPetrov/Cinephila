using AutoMapper;
using Cinephila.DataAccess.Entities;
using Cinephila.Domain.DTOs.ProductionDTOs;
using Cinephila.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
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
            var productionEntity = new ProductionEntity();
            _mapper.Map(dto, productionEntity);
            _context.Productions.Add(productionEntity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return productionEntity.ID;
        }

        public async Task UpdateAsync(Production dto, int id)
        {
            var productionEntity = await _context.Productions.Where(x => x.ID == id).FirstOrDefaultAsync().ConfigureAwait(false);
            _mapper.Map(dto, productionEntity);
            _context.Productions.Update(productionEntity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
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

        public async Task<IEnumerable<Production>> GetPaginatedAsync(int page, int size)
        {
            var productions = await _context.Productions
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync()
                .ConfigureAwait(false);

            var result = _mapper.Map<List<Production>>(productions);
            return result;
        }
    }
}
