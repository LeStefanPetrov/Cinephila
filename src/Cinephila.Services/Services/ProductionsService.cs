using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinephila.Domain.DTOs.ProductionDTOs;
using Cinephila.Domain.Redis;
using Cinephila.Domain.Repositories;
using Cinephila.Domain.Services;

namespace Cinephila.Services.Services
{
    public class ProductionsService : IProductionsService
    {
        private readonly IProductionsRepository _productionsRepository;
        private readonly IRedisRepository _redisRepository;

        public ProductionsService(IProductionsRepository productionsRepository, IRedisRepository redisRepository)
        {
            _productionsRepository = productionsRepository;
            _redisRepository = redisRepository;
        }

        public Task<int> CreateAsync(Production dto)
        {
           return _productionsRepository.CreateAsync(dto);
        }

        public Task UpdateAsync(Production dto, int id)
        {
            return _productionsRepository.UpdateAsync(dto, id);
        }

        public Task DeleteAsync(int id)
        {
            return _productionsRepository.DeleteAsync(id);
        }

        public Task<bool> CheckIfExistAsync(int id)
        {
            return _productionsRepository.CheckIfExistAsync(id);
        }

        public Task<IEnumerable<Production>> GetPaginatedAsync(int page, int size)
        {
            return _productionsRepository.GetPaginatedAsync(page, size);
        }

        public async Task<IEnumerable<Production>> GetTopPicksAsync(int page, int size)
        {
            var topPicks = await _redisRepository.GetObjectAsync<IEnumerable<Production>>("topPicks");

            if (topPicks != null)
                return topPicks;

            topPicks = await _productionsRepository.GetPaginatedAsync(page, size);
            await _redisRepository.SetObjectAsync("topPicks", topPicks, DateTime.Today.AddDays(1));

            return topPicks;
        }
    }
}
