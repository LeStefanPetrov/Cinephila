using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class GenreProductionEntity
    {
        [Required]
        public int ProductionID { get; set; }

        [Required]
        public int GenreID { get; set; }

        public virtual ProductionEntity Production { get; set; }

        public virtual GenreEntity Genre { get; set; }
    }
}
