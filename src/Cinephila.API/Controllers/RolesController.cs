using Cinephila.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinephila.API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _service;

        public RolesController(IRolesService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(string roleName)
        {
            var roleId = await _service.CreateAsync(roleName).ConfigureAwait(false);
            return roleId;
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _service.CheckIfExistAsync(id).ConfigureAwait(false))
                return NotFound();

            await _service.DeleteAsync(id).ConfigureAwait(false);
            return NoContent();
        }
    }
}
