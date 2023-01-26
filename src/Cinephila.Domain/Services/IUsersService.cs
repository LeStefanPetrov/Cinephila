using Cinephila.Domain.DTOs.UserDTOs;
using System.Threading.Tasks;

namespace Cinephila.Domain.Services
{
    public interface IUsersService
    {
        Task<int> CreateAsync(UserInfo dto);

        Task<bool> CheckIfExistAsync(string email);

        Task<UserInfo> GetProfileInfo(string email);
    }
}
