using AutoMapper;
using Cinephila.DataAccess.Entities;
using Cinephila.Domain.DTOs.FetchDataDTOs;
using Cinephila.Domain.DTOs.ProductionDTOs;
using Cinephila.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinephila.DataAccess.Repositories
{
    public class ProductionsRepository : IProductionsRepository
    {
        private readonly CinephilaDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductionsRepository> _logger;

        public ProductionsRepository(
            CinephilaDbContext context,
            IMapper mapper,
            ILogger<ProductionsRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
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
            _context.Entry(productionEntity).CurrentValues.SetValues(dto);

            foreach(var country in productionEntity.Countries)
            {
                if (!dto.Countries.Any(c => c == country.CountryID))
                    _context.CountriesProductions.Remove(country);
            }

            foreach(var participant in productionEntity.ParticipantsProductions)
            {
                if (!dto.Participants.Any(p => p.ParticipantID == participant.ParticipantID && p.RoleID == participant.RoleID))
                    _context.ParticipantsProductions.Remove(participant);
            }

            foreach (var countryID in dto.Countries)
            {
               if(!productionEntity.Countries.Any(c => c.CountryID == countryID))
                {
                    productionEntity.Countries.Add(
                        new CountryProductionEntity
                        {
                            CountryID = countryID,
                            ProductionID = productionEntity.ID
                        });
                }
            }

            foreach(var participant in dto.Participants)
            {
                if(!productionEntity.ParticipantsProductions.Any(p => p.ParticipantID == participant.ParticipantID && p.RoleID == participant.RoleID))
                {
                    productionEntity.ParticipantsProductions.Add(
                        new ParticipantProductionEntity
                        {
                            ParticipantID = participant.ParticipantID,
                            RoleID = participant.RoleID,
                            ProductionID = productionEntity.ID
                        });
                }
            }

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
                .Where(x => x.Movie != null)
                .OrderBy(x => x.ID)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync()
                .ConfigureAwait(false);

            var result = _mapper.Map<List<Production>>(productions);
            return result;
        }

        public async Task BatchInsertMovieProductionsAsync(IEnumerable<MovieDto> movieDtos)
        {
            try
            {
                if (movieDtos != null && movieDtos.Any())
                {
                    var entities = _mapper.Map<List<ProductionEntity>>(movieDtos);

                    await _context.Productions.AddRangeAsync(entities);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while inserting productions.");
            }
        }
    }
}
