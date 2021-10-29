using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.Domain.DTOs.ProductionDTOs
{
    public class MovieDto : BaseProductionDto
    {
        public int LengthInMinutes { get; set; }
    }
}
