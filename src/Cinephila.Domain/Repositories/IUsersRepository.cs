using System.Threading.Tasks;
using Cinephila.Domain.DTOs.UserDTOs;

namespace Cinephila.Domain.Repositories
{
    public interface IUsersRepository
    {
        Task<int> CreateAsync(UserInfo dto);

        Task<bool> CheckIfExistAsync(string email);

        Task<UserInfo> GetProfileInfo(string email);
    }
}
