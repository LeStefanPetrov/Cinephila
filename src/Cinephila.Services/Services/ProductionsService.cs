using Cinephila.Domain.DTOs.ProductionDTOs;
using Cinephila.Domain.Repositories;
using Cinephila.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Task DeleteAsync(int id)
        {
            return _productionsRepository.DeleteAsync(id);
        }
    }
}
