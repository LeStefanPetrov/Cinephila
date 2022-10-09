using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.Domain.Repositories
{
    public interface IUsersRepository
    {
        Task<int> CreateAsync(string email);

        Task<bool> CheckIfExistAsync(string email);
    }
}
