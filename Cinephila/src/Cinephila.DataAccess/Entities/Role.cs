using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class Role : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
