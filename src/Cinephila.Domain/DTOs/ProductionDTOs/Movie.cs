using Cinephila.Domain.ModelInterfaces;

namespace Cinephila.Domain.DTOs.ProductionDTOs
{
    public class Movie : Production, IMovie
    {
        public int LengthInMinutes { get; set; }
    }
}
