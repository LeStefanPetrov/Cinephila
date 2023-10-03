using Cinephila.Domain.DTOs.ParticipantDTOs;
using Cinephila.Domain.ModelInterfaces;
using System;
using System.Collections.Generic;

namespace Cinephila.Domain.DTOs.ProductionDTOs
{
    public class Production : IProduction
    {
        public string Name { get; set; }

        public DateTime? YearOfCreation { get; set; }

        public string Summary { get; set; }

        public string PosterPath { get; set; }

        public ICollection<int> Countries { get; set; }

        public ICollection<ParticipantRole> Participants { get; set; }
    }
}
