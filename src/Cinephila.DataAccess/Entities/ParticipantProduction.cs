using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class ParticipantProduction
    {
        [Required]
        public int ProductionID { get; set; }

        public int ParticipantID { get; set; }

        public int RoleID { get; set; }

        public virtual Production Production { get; set; }

        public virtual Participant Participant { get; set; }

        public virtual Role Role { get; set; }
    }
}
