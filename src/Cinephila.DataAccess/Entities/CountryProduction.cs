using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class CountryProduction
    {
        [Required]
        public int ProductionID { get; set; }

        [Required]
        public int CountryID { get; set; }

        public virtual Production Production { get; set; }

        public virtual Country Country { get; set; }
    }
}
