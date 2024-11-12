using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class MovieEntity
    {
        [Required]
        public int ProductionID { get; set; }

        public virtual ProductionEntity Production { get; set; }

        public int Runtime { get; set; }
    }
}
