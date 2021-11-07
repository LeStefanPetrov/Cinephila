using Cinephila.Domain.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.Domain.Models.ProductionModels
{
    public class MovieModel : ProductionModel, IMovie
    {
        public int LengthInMinutes { get; set; }
    }
}
