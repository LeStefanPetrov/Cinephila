using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class CountryProductionEntity
    {
        [Required]
        public int ProductionID { get; set; }

        [Required]
        public int CountryID { get; set; }

        public virtual ProductionEntity Production { get; set; }

        public virtual CountryEntity Country { get; set; }
    }
}
