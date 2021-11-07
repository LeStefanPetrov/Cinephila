using Cinephila.Domain.ModelInterfaces;
using Cinephila.Domain.Models.ParticipantModels;
using System;
using System.Collections.Generic;

namespace Cinephila.Domain.Models.ProductionModels
{
    public abstract class ProductionModel : IProduction
    {
        public string Name { get; set; }

        public DateTime YearOfCreation { get; set; }

        public string Summary { get; set; }

        public ICollection<int> Countries { get; set; }

        public ICollection<ParticipantRoleModel> Participants { get; set; }
    }
}
