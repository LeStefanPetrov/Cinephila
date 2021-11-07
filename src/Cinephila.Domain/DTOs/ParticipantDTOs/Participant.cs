using Cinephila.Domain.ModelInterfaces;
using System;

namespace Cinephila.Domain.DTOs.ParticipantDTOs
{
    public class Participant : IParticipant
    {
        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? DeathDate { get; set; }

        public string PlaceOfBirth { get; set; }
    }
}
