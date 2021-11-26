using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class ProductionEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public DateTime? YearOfCreation { get; set; }

        public string Summary { get; set; }

        public virtual ICollection<CountryProductionEntity> Countries { get; set; }

        public virtual ICollection<ParticipantProductionEntity> ParticipantsProductions { get; set; }

        public virtual MovieEntity Movie { get; set; }

        public virtual TVShowEntity TVShow { get; set; }
    }
}
