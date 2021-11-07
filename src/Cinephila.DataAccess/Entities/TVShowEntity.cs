using System;
using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class TVShowEntity : BaseEntity
    {
        [Required]
        public int ProductionID { get; set; }

        public virtual ProductionEntity Production { get; set; }

        public DateTime? EndOfProduction { get; set; }
    }
}
