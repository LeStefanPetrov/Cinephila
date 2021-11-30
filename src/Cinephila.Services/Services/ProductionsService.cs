using Cinephila.Domain.DTOs.ProductionDTOs;
using Cinephila.Domain.Repositories;
using Cinephila.Domain.Services;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinephila.Services.Services
{
    public class ProductionsService : IProductionsService
    {
        private readonly IProductionsRepository _productionsRepository;

        public ProductionsService(IProductionsRepository productionsRepository)
        {
            _productionsRepository = productionsRepository;
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
    }
}
