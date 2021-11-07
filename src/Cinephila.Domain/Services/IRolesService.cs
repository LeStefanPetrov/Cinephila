using System.Threading.Tasks;

namespace Cinephila.Domain.Services
{
    public interface IRolesService
    {
        Task<int> CreateAsync(string roleName);

        Task DeleteAsync(int id);

        Task<bool> CheckIfExistAsync(int id);
    }
}
