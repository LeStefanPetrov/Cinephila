using Cinephila.Domain.DTOs.ProductionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.Domain.Services
{
    public interface IProductionsService
    {
        Task<int> CreateAsync(ProductionDto dto);
    }
}
