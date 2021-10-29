using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.Domain.DTOs
{
    public class ProductionDto
    {
        public ProductionType ProductionType { get; set; }

        public Production Production { get; set; }
    }

    public abstract class Production
    {
        public string Name { get; set; }
    }

    public class Movie : Production
    { 
        public int Length { get; set; }
    }

    public class TVShow : Production { }

    public enum ProductionType
    {
        TVShow,
        Movie
    }
}
