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
        private readonly IRolesRepository _repository;

        public RolesService(IRolesRepository repository)
        {
            _repository = repository;            
        }

        public Task<int> CreateAsync(string roleName)
        {
            return _repository.CreateAsync(roleName);
        }

        public Task DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public Task<bool> CheckIfExistAsync(int id)
        {
            return  _repository.CheckIfExistAsync(id);
        }
    }
}
