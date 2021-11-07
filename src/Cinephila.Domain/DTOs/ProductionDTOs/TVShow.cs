using Cinephila.Domain.ModelInterfaces;
using System;

namespace Cinephila.Domain.DTOs.ProductionDTOs
{
    public class TVShow : Production, ITVShow
    {
        public DateTime EndOfProduction { get; set; }
    }
}
