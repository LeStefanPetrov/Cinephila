using Cinephila.Domain.Repositories;
using Cinephila.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.Services.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;

        public RolesService(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;            
        }

        public Task<int> CreateAsync(string roleName)
        {
            return _rolesRepository.CreateAsync(roleName);
        }

        public Task DeleteAsync(int id)
        {
            return _rolesRepository.DeleteAsync(id);
        }

        public Task<bool> CheckIfExistAsync(int id)
        {
            return _rolesRepository.CheckIfExistAsync(id);
        }
    }
}
