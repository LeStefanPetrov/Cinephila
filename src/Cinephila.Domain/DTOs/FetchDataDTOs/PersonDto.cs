using System;

namespace Cinephila.Domain.DTOs.FetchDataDTOs
{
    public class PersonDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Popularity { get; set; }

        public DateOnly BirthDay { get; set; }

        public DateOnly? DeathDay { get; set; }
    }
}
