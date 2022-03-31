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
    public class UsersRepository : IUsersRepository
    {
        private readonly CinephilaDbContext _context;

        public UsersRepository(CinephilaDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(string email)
        {
            var entity = new UserEntity { Username = email , Email = email, Password = email};

            _context.Users.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return entity.ID;
        }

        public async Task<bool> CheckIfExistAsync(string email)
        {
            return await _context.Users.AnyAsync(x => x.Username == email).ConfigureAwait(false);
        }
    }
}
