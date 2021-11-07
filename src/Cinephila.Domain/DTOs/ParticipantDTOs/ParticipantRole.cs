using Cinephila.Domain.ModelInterfaces;

namespace Cinephila.Domain.DTOs.ParticipantDTOs
{
    public class ParticipantRole : IParticipantRole
    {
        public int ParticipantID { get; set; }

        public int RoleID { get; set; }
    }
}
