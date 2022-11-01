using Cinephila.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cinephila.API.Controllers
{
    [Route("api/signIn")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<ActionResult> SignIn()
        {
            var identity = (User.Identity as ClaimsIdentity);
            var email = identity.FindFirst("email")?.Value;

            if (email == null)
                return BadRequest("No such claim!");

            if (await _usersService.CheckIfExistAsync(email))
                return Ok();


            var id = await _usersService.CreateAsync(email);

            return Ok(id);
        }
    }
}
