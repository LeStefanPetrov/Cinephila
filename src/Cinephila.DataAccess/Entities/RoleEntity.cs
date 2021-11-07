using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class RoleEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<ParticipantProductionEntity> ParticipantsProductions { get; set; }
    }
}
