using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class CountryEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<CountryProductionEntity> Productions { get; set; }
    }
}
