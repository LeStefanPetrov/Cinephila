using Cinephila.DataAccess.Entities;
using Cinephila.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.DataAccess.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        protected readonly CinephilaDbContext _context;

        public RolesRepository(CinephilaDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(string roleName)
        {
            var role = new Role { Name = roleName };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return role.ID;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Roles.FirstOrDefaultAsync(x => x.ID == id).ConfigureAwait(false);
            _context.Roles.Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<bool> CheckIfExistAsync(int id)
        {
            return await _context.Roles.AnyAsync(x => x.ID == id).ConfigureAwait(false);
        }

    }
}
