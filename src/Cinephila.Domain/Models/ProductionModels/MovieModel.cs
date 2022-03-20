using Cinephila.Domain.ModelInterfaces;

namespace Cinephila.Domain.Models.ProductionModels
{
    public class MovieModel : ProductionModel, IMovie
    {
        public int LengthInMinutes { get; set; }
    }
}
