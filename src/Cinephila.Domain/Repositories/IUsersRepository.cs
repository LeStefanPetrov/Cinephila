using Cinephila.Domain.DTOs.UserDTOs;
using System.Threading.Tasks;

namespace Cinephila.Domain.Repositories
{
    public interface IUsersRepository
    {
        Task<int> CreateAsync(UserInfo dto);

        Task<bool> CheckIfExistAsync(string email);
    }
}
