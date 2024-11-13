using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class ProductionEntity : BaseEntity
    {
        public string? Name { get; set; }

        public string Title { get; set; }

        public string OriginalTitle { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string Summary { get; set; }

        [Required]
        public int TmdbID { get; set; }

        public string PosterPath { get; set; }

        public double Popularity { get; set; }

        public int Budget { get; set; }

        public long Revenue { get; set; }

        public double VoteAverage { get; set; }

        public int VoteCount { get; set; }

        public virtual ICollection<CountryProductionEntity> Countries { get; set; } = new List<CountryProductionEntity>();

        public virtual ICollection<ParticipantProductionEntity> ParticipantsProductions { get; set; } = new List<ParticipantProductionEntity>();

        public virtual ICollection<GenreProductionEntity> GenresProductions { get; set; } = new List<GenreProductionEntity>();

        public virtual MovieEntity Movie { get; set; }

        public virtual TVShowEntity TVShow { get; set; }
    }
}
