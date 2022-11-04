using Cinephila.Domain.DTOs.UserDTOs;
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
            ClaimsIdentity identity = (User.Identity as ClaimsIdentity);
            string email = identity.FindFirst("email")?.Value;

            if (email == null)
                return BadRequest("No such claim!");

            if (await _usersService.CheckIfExistAsync(email))
                return Ok();

            UserInfo dto = new UserInfo
            {
                Email = email,
                Picture = identity.FindFirst("picture")?.Value,
                FirstName = identity.FindFirst("given_name")?.Value,
                LastName = identity.FindFirst("family_name")?.Value
            };

            var id = await _usersService.CreateAsync(dto).ConfigureAwait(false);

            return Ok(id);
        }
    }
}
