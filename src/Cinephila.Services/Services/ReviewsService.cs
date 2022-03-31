using Cinephila.Domain.DTOs.ReviewDTOs;
using Cinephila.Domain.Repositories;
using Cinephila.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.Services.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly IReviewsRepository _reviewsRepository;

        public ReviewsService(IReviewsRepository reviewsRepository)
        {
            _reviewsRepository = reviewsRepository;
        }

        public Task<int> CreateAsync(Review dto)
        {
            return _reviewsRepository.CreateAsync(dto);
        }

        public Task UpdateAsync(Review dto, int id)
        {
            return _reviewsRepository.UpdateAsync(dto, id);
        }

        public Task DeleteAsync(int id)
        {
            return _reviewsRepository.DeleteAsync(id);
        }

        public Task<List<Review>> GetPaginatedAsync(int currentPage, int pageSize)
        {
            return _reviewsRepository.GetPaginatedAsync(currentPage, pageSize);
        }

        public Task<bool> CheckIfExistAsync(int id)
        {
            return _reviewsRepository.CheckIfExistAsync(id);
        }
    }
}
