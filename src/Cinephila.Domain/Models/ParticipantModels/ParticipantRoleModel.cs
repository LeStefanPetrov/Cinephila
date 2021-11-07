using Cinephila.Domain.ModelInterfaces;

namespace Cinephila.Domain.Models.ParticipantModels
{
    public class ParticipantRoleModel : IParticipantRole
    {
        public int ParticipantID { get; set; }

        public int RoleID { get; set; }
    }
}
