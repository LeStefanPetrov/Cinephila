using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class ParticipantProduction
    {
        [Required]
        public int ProductionID { get; set; }

        public int ParticipantID { get; set; }

        public int RoleID { get; set; }

        public Production Production { get; set; }

        public Participant Participant { get; set; }

        public Role Role { get; set; }
    }
}
