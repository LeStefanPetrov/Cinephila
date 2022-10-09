using System.Runtime.Serialization;

namespace Cinephila.Domain.DTOs.ApiDTOs
{
    [DataContract]
    public class PopularMoviesDto
    {
        [DataMember(Name = "page")]
        public int Page { get; set; }

        [DataMember(Name = "results")]
        public MovieDto[] Movies { get; set; }
    }
}
