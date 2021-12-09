using System;
using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class TVShowEntity
    {
        [Required]
        public int ProductionID { get; set; }

        public virtual ProductionEntity Production { get; set; }

        public DateTime? EndOfProduction { get; set; }
    }
}
