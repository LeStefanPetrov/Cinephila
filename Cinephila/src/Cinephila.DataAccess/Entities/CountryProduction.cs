using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class CountryProduction
    {
        [Required]
        public int ProductionID { get; set; }

        [Required]
        public int CountryID { get; set; }

        public Production Production { get; set; }

        public Country Country { get; set; }
    }
}
