using Cinephila.API.DataBinding;
using Cinephila.Domain.DTOs;
using Cinephila.Domain.DTOs.ProductionDTOs;
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
        public ProductionsController()
        {
        }

        [HttpPost]
        public ActionResult Create([ModelBinder(BinderType = typeof(ProductionModelBinder))] ProductionDto productionDto)
        {
            if (productionDto == null)
                return BadRequest();

            var a = productionDto;
            return Ok(productionDto);
        }
    }
}
