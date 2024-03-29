﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class ParticipantEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? DeathDate { get; set; }

        public string PlaceOfBirth { get; set; }

        public virtual ICollection<ParticipantProductionEntity> ParticipantsProductions { get; set; }
    }
}
