using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.Domain.DTOs.ProductionDTOs
{
    public abstract class BaseProductionDto
    {
        public string Name { get; set; }

        public DateTime YearOfCreation { get; set; }

        public string Summary { get; set; }
    }
}
