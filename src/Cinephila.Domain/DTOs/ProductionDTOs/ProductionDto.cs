using Cinephila.Domain.DTOs.ProductionDTOs;
using Cinephila.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.Domain.DTOs
{
    public class ProductionDto
    {
        public ProductionType ProductionType { get; set; }

        public BaseProductionDto Production { get; set; }
    }
}
