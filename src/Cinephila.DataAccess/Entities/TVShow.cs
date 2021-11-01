using System;
using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class TVShow : BaseEntity
    {
        [Required]
        public int ProductionID { get; set; }

        public virtual Production Production { get; set; }

        public DateTime? EndOfProduction { get; set; }
    }
}
