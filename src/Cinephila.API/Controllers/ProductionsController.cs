using AutoMapper;
using Cinephila.Domain.DTOs.ProductionDTOs;
using Cinephila.Domain.Models.ProductionModels;
using Cinephila.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Cinephila.API.Controllers
{
    [Route("api/productions")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ProductionsController : Controller
    {
        private readonly IProductionsService _productionsService;
        private readonly IMapper _mapper;

        public ProductionsController(IProductionsService productionsService, IMapper mapper)
        {
            _productionsService = productionsService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(ProductionCreateModel model)
        {
            return Ok(await _productionsService.CreateAsync(_mapper.Map<Production>(model)).ConfigureAwait(false));
        }

        [HttpPut]
        public async Task<ActionResult<int>> Update(int id, ProductionCreateModel model)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _productionsService.CheckIfExistAsync(id).ConfigureAwait(false))
                return BadRequest();

            if (model.Movie != null)
            {
                await _productionsService.UpdateAsync(_mapper.Map<Movie>(model.Movie), id).ConfigureAwait(false);
                return NoContent();
            }

            await _productionsService.UpdateAsync(_mapper.Map<TVShow>(model.TVShow), id).ConfigureAwait(false);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            await _productionsService.DeleteAsync(id).ConfigureAwait(false);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult> GetPaginatedAsync(int page, int size)
        {
            var productions = await _productionsService.GetPaginatedAsync(page, size).ConfigureAwait(false);

            if (!productions.Any())
                return NotFound();

            return Ok(productions);
        }
    }
}
