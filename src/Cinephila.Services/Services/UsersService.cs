using Cinephila.Domain.DTOs.UserDTOs;
using Cinephila.Domain.Repositories;
using Cinephila.Domain.Services;
using System.Threading.Tasks;

namespace Cinephila.Services.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Task<int> CreateAsync(UserInfo dto)
        {
            return _usersRepository.CreateAsync(dto);
        }

        public Task<bool> CheckIfExistAsync(string email)
        {
            return _usersRepository.CheckIfExistAsync(email);
        }

        public Task<UserInfo> GetProfileInfo(string email)
        {
            return _usersRepository.GetProfileInfo(email);
        }
    }
}
