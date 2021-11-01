using Cinephila.Domain.DTOs.ParticipantDTOs;
using Cinephila.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Cinephila.API.Controllers
{
    [Route("api/participants")]
    [ApiController]
    public class ParticipantsController : Controller
    {
        private readonly IParticipantsService _participantsService;

        public ParticipantsController(IParticipantsService participantsService)
        {
            _participantsService = participantsService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(ParticipantDto dto)
        {
            var participantId = await _participantsService.CreateAsync(dto).ConfigureAwait(false);
            return Created(Request.Path.Value, participantId);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ParticipantDto dto, int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _participantsService.CheckIfExistAsync(id).ConfigureAwait(false))
                return NotFound();

            await _participantsService.UpdateAsync(dto, id).ConfigureAwait(false);

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

            return Ok(participants);
        }
    }
}
