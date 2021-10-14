using Cinephila.Domain.DTOs.ParticipantsDTOs;
using Cinephila.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinephila.API.Controllers
{
    [Route("api/participants")]
    [ApiController]
    public class ParticipantsController : Controller
    {
        private readonly IParticipantsService _service;

        public ParticipantsController(IParticipantsService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(ParticipantDto dto)
        {
            var participantId = await _service.CreateAsync(dto).ConfigureAwait(false);
            return Created(Request.Path.Value, participantId);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ParticipantDto dto, int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _service.CheckIfExistAsync(id).ConfigureAwait(false))
                return NotFound();

            await _service.UpdateAsync(dto, id).ConfigureAwait(false);

            return NoContent();
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

        [HttpGet]
        public async Task<ActionResult> GetPaginated(int currentPage, int pageSize)
        {
            var participants = await _service.GetPaginatedAsync(currentPage, pageSize).ConfigureAwait(false);

            if (!participants.Any())
                return NotFound();

            return Ok(participants);
        }
    }
}
