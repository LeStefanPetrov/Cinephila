using AutoMapper;
using Cinephila.Domain.DTOs.UserDTOs;
using Cinephila.Domain.Models.UserModels;
using Cinephila.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cinephila.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;


        public UsersController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> ProfileInfo()
        {
            ClaimsIdentity identity = (User.Identity as ClaimsIdentity);
            string email = identity.FindFirst("email")?.Value;

        if (email == null)
            return BadRequest("No such claim!");

            var profileInfo = await _usersService.GetProfileInfo(email).ConfigureAwait(false);

            if (profileInfo != null)
                return Ok(_mapper.Map<UserInfoModel>(profileInfo));

            var newProfileInfo = new UserInfo
            {
                Email = email,
                Picture = identity.FindFirst("picture")?.Value,
                FirstName = identity.FindFirst("given_name")?.Value,
                LastName = identity.FindFirst("family_name")?.Value
            };

            await _usersService.CreateAsync(newProfileInfo).ConfigureAwait(false);

            return Ok(_mapper.Map<UserInfoModel>(newProfileInfo));
        }
    }
}
