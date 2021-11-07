using Cinephila.Domain.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.Domain.Models.ParticipantModels
{
    public class ParticipantRoleModel : IParticipantRole
    {
        public int ParticipantID { get; set; }

        public int RoleID { get; set; }
    }
}
