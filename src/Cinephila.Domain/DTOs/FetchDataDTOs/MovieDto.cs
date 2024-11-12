using System;
using System.Collections.Generic;

namespace Cinephila.Domain.DTOs.FetchDataDTOs
{
    public class MovieDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public int Budget { get; set; }

        public List<GenreDto> Genres { get; set; }

        public string Original_Title { get; set; }

        public string Overview { get; set; }

        public double Popularity { get; set; }

        public string Poster_Path { get; set; }

        public string Release_Date { get; set; }

        public long Revenue { get; set; }

        public int Runtime { get; set; }

        public double Vote_Average { get; set; }

        public int Vote_Count { get; set; }
    }
}
