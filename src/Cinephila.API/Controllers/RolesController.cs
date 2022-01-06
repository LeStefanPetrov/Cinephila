using Cinephila.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cinephila.API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(string roleName)
        {
            var roleId = await _rolesService.CreateAsync(roleName).ConfigureAwait(false);
            return Created(Request.Path.Value, roleId);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _rolesService.CheckIfExistAsync(id).ConfigureAwait(false))
                return NotFound();

            await _rolesService.DeleteAsync(id).ConfigureAwait(false);
            return NoContent();
        }
    }
}
