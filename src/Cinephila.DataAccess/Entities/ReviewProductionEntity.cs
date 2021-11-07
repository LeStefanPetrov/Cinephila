using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class ReviewProductionEntity
    {
        [Required]
        public int ProductionID { get; set; }

        [Required]
        public int UserID { get; set; }

        public string Review { get; set; }

        public int? Rating { get; set; }

        public virtual ProductionEntity Production { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
