using AutoMapper;
using Cinephila.Domain.DTOs.ParticipantDTOs;
using Cinephila.Domain.Models.ParticipantModels;
using Cinephila.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cinephila.API.Controllers
{
    [Route("api/participants")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ParticipantsController : Controller
    {
        private readonly IParticipantsService _participantsService;
        private readonly IMapper _mapper;

        public ParticipantsController(IParticipantsService participantsService, IMapper mapper)
        {
            _participantsService = participantsService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(ParticipantModel model)
        {
            var participantId = await _participantsService.CreateAsync(_mapper.Map<Participant>(model)).ConfigureAwait(false);
            return Created(Request.Path.Value, participantId);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ParticipantModel model, int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _participantsService.CheckIfExistAsync(id).ConfigureAwait(false))
                return NotFound();

            await _participantsService.UpdateAsync(_mapper.Map<Participant>(model), id).ConfigureAwait(false);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _participantsService.CheckIfExistAsync(id).ConfigureAwait(false))
                return NotFound();

            await _participantsService.DeleteAsync(id).ConfigureAwait(false);

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult> GetPaginated(int currentPage, int pageSize)
        {
            var participants = await _participantsService.GetPaginatedAsync(currentPage, pageSize).ConfigureAwait(false);

            if (!participants.Any())
                return NotFound();

            return Ok(_mapper.Map<List<ParticipantModel>>(participants));
        }
    }
}
