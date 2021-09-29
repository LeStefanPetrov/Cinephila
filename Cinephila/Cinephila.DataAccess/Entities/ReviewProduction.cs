using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class ReviewProduction
    {
        [Required]
        public int ProductionID { get; set; }

        [Required]
        public int UserID { get; set; }

        public string Review { get; set; }

        public int? Rating { get; set; }

        public Production Production { get; set; }

        public User User { get; set; }
        
    }
}
