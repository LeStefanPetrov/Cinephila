using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }
    }
}
