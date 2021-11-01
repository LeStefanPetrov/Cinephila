using AutoMapper;
using Cinephila.DataAccess.Entities;
using Cinephila.Domain.DTOs.ProductionDTOs;
using Cinephila.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<int> CreateAsync(ProductionDto dto)
        {
            if (dto is MovieDto)
            {
                var movie = _mapper.Map<Movie>(dto);

                _context.Movies.Add(movie);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return movie.ID;
            }
            else
            {
                var show = _mapper.Map<TVShow>(dto);
                _context.TVShows.Add(show);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return show.ID;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _context.Productions.FirstOrDefault(x => x.ID == id);
            _context.Productions.Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
