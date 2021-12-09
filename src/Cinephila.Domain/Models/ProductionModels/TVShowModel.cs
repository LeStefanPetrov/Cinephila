using Cinephila.Domain.ModelInterfaces;
using System;

namespace Cinephila.Domain.Models.ProductionModels
{
    public class TVShowModel : ProductionModel, ITVShow
    {
        public DateTime? EndOfProduction { get; set; }
    }
}
