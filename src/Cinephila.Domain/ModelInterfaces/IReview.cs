using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.Domain.ModelInterfaces
{
    public interface IReview
    {
        int ProductionID { get; set; }

        int UserID { get; set; }

        string UserReview { get; set; }

        int Rating { get; set; }
    }
}
