using BLL.Services;
using HerdSync.Shared.DTO.Program;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Program.ProgramRun
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramRunTreatmentController : ControllerBase
    {
        private readonly IProgramRunTreatmentService _service;
        public ProgramRunTreatmentController(IProgramRunTreatmentService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{programRunTreatmentId:Guid}")]
        public async Task<IActionResult> GetById(Guid programRunTreatmentId)
        {
            var result = await _service.GetByIdAsync(programRunTreatmentId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProgramRunTreatmentDTO programRunTreatmentDTO)
        {
            var result = await _service.CreateAsync(programRunTreatmentDTO);
            return CreatedAtAction(nameof(GetById), new { programRunTreatmentId = result.ProgramRunTreatmentId }, result);
        }

        [HttpPut("{programRunTreatmentId:Guid}")]
        public async Task<IActionResult> Update(Guid programRunTreatmentId, [FromBody] ProgramRunTreatmentDTO programRunTreatmentDTO)
        {
            if (programRunTreatmentId != programRunTreatmentDTO.ProgramRunTreatmentId) return BadRequest("ID mismatch.");
            var result = await _service.UpdateAsync(programRunTreatmentDTO);
            return Ok(result);
        }

        [HttpDelete("{programRunTreatmentId:Guid}")]
        public async Task<IActionResult> SoftDelete(Guid programRunTreatmentId)
        {
            await _service.SoftDeleteAsync(programRunTreatmentId);
            return NoContent();
        }
    }
}