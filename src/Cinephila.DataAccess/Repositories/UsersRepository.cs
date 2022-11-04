using AutoMapper;
using Cinephila.DataAccess.Entities;
using Cinephila.Domain.DTOs.UserDTOs;
using Cinephila.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cinephila.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly CinephilaDbContext _context;
        private readonly IMapper _mapper;


        public UsersRepository(CinephilaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(UserInfo dto)
        {
            var entity = _mapper.Map<UserEntity>(dto);

            _context.Users.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return entity.ID;
        }

        public async Task<bool> CheckIfExistAsync(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email).ConfigureAwait(false);
        }
    }
}
