using AutoMapper;
using Cinephila.Domain.DTOs.ReviewDTOs;
using Cinephila.Domain.Models.ReviewModels;
using Cinephila.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinephila.API.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReviewsController : Controller
    {
        private readonly IReviewsService _reviewsService;
        private readonly IMapper _mapper;

        public ReviewsController(IReviewsService reviewsService, IMapper mapper)
        {
            _reviewsService = reviewsService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(ReviewModel model)
        {
            var participantId = await _reviewsService.CreateAsync(_mapper.Map<Review>(model)).ConfigureAwait(false);
            return Created(Request.Path.Value, participantId);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ReviewModel model, int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _reviewsService.CheckIfExistAsync(id).ConfigureAwait(false))
                return NotFound();

            await _reviewsService.UpdateAsync(_mapper.Map<Review>(model), id).ConfigureAwait(false);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _reviewsService.CheckIfExistAsync(id).ConfigureAwait(false))
                return NotFound();

            await _reviewsService.DeleteAsync(id).ConfigureAwait(false);

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult> GetPaginated(int currentPage, int pageSize)
        {
            var participants = await _reviewsService.GetPaginatedAsync(currentPage, pageSize).ConfigureAwait(false);

            if (!participants.Any())
                return NotFound();

            return Ok(_mapper.Map<List<ReviewModel>>(participants));
        }
    }
}
