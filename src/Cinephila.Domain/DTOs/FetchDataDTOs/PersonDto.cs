namespace Cinephila.Domain.DTOs.FetchDataDTOs
{
    public class PersonDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Popularity { get; set; }

        public string Biography { get; set; }

        public string BirthDay { get; set; }

        public string? DeathDay { get; set; }

        public string Known_For_Department { get; set; }

        public string Place_Of_Birth { get; set; }

        public CastResponse Movie_Credits { get; set; }

        public ImageResponse Images { get; set; }
    }
}
