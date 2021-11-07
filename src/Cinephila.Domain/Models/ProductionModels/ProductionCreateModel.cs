using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.Domain.Models.ProductionModels
{
    public class ProductionCreateModel
    {
        public MovieModel Movie { get; set; }

        public TVShowModel TVShow { get; set; }
    }
}
