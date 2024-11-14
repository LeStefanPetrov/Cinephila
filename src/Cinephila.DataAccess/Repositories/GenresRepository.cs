using AutoMapper;
using Cinephila.DataAccess.Entities;
using Cinephila.Domain.DTOs.FetchDataDTOs;
using Cinephila.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinephila.DataAccess.Repositories
{
    public class GenresRepository : IGenresRepository
    {
        private readonly CinephilaDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GenresRepository> _logger;

        public GenresRepository(
            CinephilaDbContext context,
            IMapper mapper,
            ILogger<GenresRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task BatchInsertGenresAsync(IEnumerable<GenreDto> genreDtos)
        {
            if (genreDtos != null && genreDtos.Any())
            {
                var entities = _mapper.Map<List<GenreEntity>>(genreDtos);

                await _context.Genres.AddRangeAsync(entities);
                try
                {
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while inserting genres.");
                }
            }
        }
    }
}
