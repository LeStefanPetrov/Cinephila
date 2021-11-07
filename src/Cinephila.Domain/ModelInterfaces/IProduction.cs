using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.Domain.ModelInterfaces
{
    public interface IProduction
    {
        string Name { get; set; }

        DateTime YearOfCreation { get; set; }

        string Summary { get; set; }

        ICollection<int> Countries { get; set; }
    }
}
