using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class Movie : BaseEntity
    {
        [Required]
        public int ProductionID { get; set; }

        public Production Production { get; set; }

        public int LengthInMinutes { get; set; }

    }
}
