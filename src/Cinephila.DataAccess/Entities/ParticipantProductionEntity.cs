using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class ParticipantProductionEntity
    {
        [Required]
        public int ProductionID { get; set; }

        [Required]
        public int ParticipantID { get; set; }

        public int? RoleID { get; set; }

        public string Character { get; set; }

        public virtual ProductionEntity Production { get; set; }

        public virtual ParticipantEntity Participant { get; set; }

        public virtual RoleEntity Role { get; set; }
    }
}
