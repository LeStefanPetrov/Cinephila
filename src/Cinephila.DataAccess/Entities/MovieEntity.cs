using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class MovieEntity : BaseEntity
    {
        [Required]
        public int ProductionID { get; set; }

        public virtual ProductionEntity Production { get; set; }

        public int LengthInMinutes { get; set; }
    }
}
