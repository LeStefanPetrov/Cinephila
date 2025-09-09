using System.Collections.Generic;
using System.Threading.Tasks;
using Cinephila.Domain.DTOs.ReviewDTOs;

namespace Cinephila.Domain.Services
{
    public interface IReviewsService
    {
        Task<int> CreateAsync(Review dto);

        Task UpdateAsync(Review dto, int id);

        Task DeleteAsync(int id);

        Task<List<Review>> GetPaginatedAsync(int currentPage, int pageSize);

        Task<bool> CheckIfExistAsync(int id);
    }
}
