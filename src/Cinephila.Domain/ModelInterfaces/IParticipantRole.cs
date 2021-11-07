using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.Domain.ModelInterfaces
{
    public interface IParticipantRole
    {
        int ParticipantID { get; set; }

        int RoleID { get; set; }
    }
}
