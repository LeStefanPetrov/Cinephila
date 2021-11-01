using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class Production : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public DateTime? YearOfCreation { get; set; }

        public string Summary { get; set; }

        public virtual ICollection<CountryProduction> Countries { get; set; }

        public virtual ICollection<ParticipantProduction> ParticipantsProductions { get; set; }
    }
}
