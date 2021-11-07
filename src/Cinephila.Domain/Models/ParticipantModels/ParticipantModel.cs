using Cinephila.Domain.ModelInterfaces;
using System;

namespace Cinephila.Domain.Models.ParticipantModels
{
    public class ParticipantModel : IParticipant
    {
        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? DeathDate { get; set; }

        public string PlaceOfBirth { get; set; }
    }
}
