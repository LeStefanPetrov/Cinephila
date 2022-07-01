using System;
using System.Runtime.Serialization;

namespace Cinephila.Domain.DTOs.ApiDTOs
{
    [DataContract]
    public class MovieDto
    {
        [DataMember(Name = "id")]
        public int ID { get; set; }

        [DataMember(Name = "title")]
        public string Name { get; set; }

        [DataMember(Name = "release_date")]
        public DateTime? ReleaseDate { get; set; }

        [DataMember(Name = "poster_path")]
        public string PosterPath { get; set; }
    }
}
