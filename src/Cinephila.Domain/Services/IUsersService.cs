using System.Threading.Tasks;

namespace Cinephila.Domain.Services
{
    public interface IUsersService
    {
        Task<int> CreateAsync(string email);

        Task<bool> CheckIfExistAsync(string email);
    }
}
