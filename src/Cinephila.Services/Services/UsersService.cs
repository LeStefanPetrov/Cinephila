using Cinephila.Domain.Repositories;
using Cinephila.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Task<int> CreateAsync(string email)
        {
            return _usersRepository.CreateAsync(email);
        }

        public Task<bool> CheckIfExistAsync(string email)
        {
            return _usersRepository.CheckIfExistAsync(email);
        }
    }
}
