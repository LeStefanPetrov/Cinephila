using System;

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
