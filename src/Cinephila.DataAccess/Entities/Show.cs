using System;
using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class Show : BaseEntity
    {
        [Required]
        public int ProductionID { get; set; }

        public Production Production { get; set; }

        public DateTime? EndOfProduction { get; set; }
    }
}
