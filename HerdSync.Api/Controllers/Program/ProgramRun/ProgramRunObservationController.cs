using BLL.Services;
using HerdSync.Shared.DTO.Program;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Program.ProgramRun
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramRunObservationController : ControllerBase
    {
        private readonly IProgramRunObservationService _service;
        public ProgramRunObservationController(IProgramRunObservationService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{programRunObservationId:Guid}")]
        public async Task<IActionResult> GetById(Guid programRunObservationId)
        {
            var result = await _service.GetByIdAsync(programRunObservationId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProgramRunObservationDTO programRunObservationDTO)
        {
            var result = await _service.CreateAsync(programRunObservationDTO);
            return CreatedAtAction(nameof(GetById), new { programRunObservationId = result.Id }, result);
        }

        [HttpPut("{programRunObservationId:Guid}")]
        public async Task<IActionResult> Update(Guid programRunObservationId, [FromBody] ProgramRunObservationDTO programRunObservationDTO)
        {
            if (programRunObservationId != programRunObservationDTO.ProgramRunObservationId) return BadRequest("ID mismatch.");
            var result = await _service.UpdateAsync(programRunObservationDTO);
            return Ok(result);
        }

        [HttpDelete("{programRunObservationId:Guid}")]
        public async Task<IActionResult> SoftDelete(Guid programRunObservationId)
        {
            await _service.SoftDeleteAsync(programRunObservationId);
            return NoContent();
        }
    }

}