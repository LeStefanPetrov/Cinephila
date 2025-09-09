using System;
using Cinephila.Domain.ModelInterfaces;

namespace Cinephila.Domain.Models.ProductionModels
{
    public class TVShowModel : ProductionModel, ITVShow
    {
        public DateTime? EndOfProduction { get; set; }
    }
}
