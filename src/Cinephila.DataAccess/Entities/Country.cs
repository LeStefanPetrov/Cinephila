using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class Country : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<CountryProduction> Productions { get; set; }
    }
}
