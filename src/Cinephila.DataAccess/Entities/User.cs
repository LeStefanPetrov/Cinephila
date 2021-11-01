using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
