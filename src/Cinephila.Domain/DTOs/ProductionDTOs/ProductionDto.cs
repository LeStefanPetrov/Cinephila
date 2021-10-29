using Cinephila.Domain.Enum;
using System;

namespace Cinephila.Domain.DTOs.ProductionDTOs
{
    public abstract class ProductionDto
    {
        public ProductionType Type { get; set; }

        public string Name { get; set; }

        public DateTime YearOfCreation { get; set; }

        public string Summary { get; set; }
    }
}
