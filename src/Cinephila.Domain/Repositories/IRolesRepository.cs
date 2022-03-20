using System.Threading.Tasks;

namespace Cinephila.Domain.Repositories
{
    public interface IRolesRepository
    {
        Task<int> CreateAsync(string roleName);

        Task DeleteAsync(int id);

        Task<bool> CheckIfExistAsync(int id);
    }
}
