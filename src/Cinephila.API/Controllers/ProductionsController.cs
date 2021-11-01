using Cinephila.API.DataBinding;
using Cinephila.Domain.DTOs;
using Cinephila.Domain.DTOs.ProductionDTOs;
using Cinephila.Domain.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinephila.API.Controllers
{
    [Route("api/productions")]
    [ApiController]
    public class ProductionsController : Controller
    {
        private readonly IProductionsService _productionsService;
        private readonly IValidator<MovieDto> _movieValidator;


        public ProductionsController(IProductionsService productionsService, IValidator<MovieDto> validator)
        {
            _productionsService = productionsService;
            _movieValidator = validator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([ModelBinder(BinderType = typeof(ProductionModelBinder))] ProductionDto productionDto)
        {
            if (productionDto == null)
                return BadRequest();

            var validationResult = new ValidationResult();

            if(productionDto.GetType() == typeof(MovieDto))
                validationResult = await _movieValidator.ValidateAsync(productionDto as MovieDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            return Ok(await _productionsService.CreateAsync(productionDto));
        }
    }
}
