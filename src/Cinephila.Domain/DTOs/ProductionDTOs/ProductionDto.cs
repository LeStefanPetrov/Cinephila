using Cinephila.Domain.DTOs.ParticipantDTOs;
using Cinephila.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Cinephila.Domain.DTOs.ProductionDTOs
{
    public abstract class ProductionDto
    {
        public ProductionType Type { get; set; }

        public string Name { get; set; }

        public DateTime YearOfCreation { get; set; }

        public string Summary { get; set; }

        public ICollection<int> Countries { get; set; }

        public ICollection<ParticipantRoleDto> Participants { get; set; }
    }
}
