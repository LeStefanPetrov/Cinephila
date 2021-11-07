using AutoMapper;
using Cinephila.Domain.DTOs.ProductionDTOs;
using Cinephila.Domain.Models.ProductionModels;
using Cinephila.Domain.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cinephila.API.Controllers
{
    [Route("api/productions")]
    [ApiController]
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
            if (model.Movie != null)
                return Ok(await _productionsService.CreateAsync(_mapper.Map<Movie>(model.Movie)).ConfigureAwait(false));


            return Ok(await _productionsService.CreateAsync(_mapper.Map<TVShow>(model.TVShow)).ConfigureAwait(false));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            await _productionsService.DeleteAsync(id).ConfigureAwait(false);
            return NoContent();
        }
    }
}
