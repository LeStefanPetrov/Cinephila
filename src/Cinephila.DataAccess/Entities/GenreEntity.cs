using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class GenreEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int TmdbId { get; set; }
    }
}
