using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.Domain.ModelInterfaces
{
    public interface IParticipant
    {
        string Name { get; set; }

        DateTime? BirthDate { get; set; }

        DateTime? DeathDate { get; set; }

        string PlaceOfBirth { get; set; }
    }
}
