using System;
using Cinephila.Domain.ModelInterfaces;

namespace Cinephila.Domain.DTOs.ProductionDTOs
{
    public class TVShow : Production, ITVShow
    {
        public DateTime? EndOfProduction { get; set; }
    }
}
