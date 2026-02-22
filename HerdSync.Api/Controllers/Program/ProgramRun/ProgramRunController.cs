using BLL.Services;
using HerdSync.Shared.DTO.Program;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Program.ProgramRun
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramRunController : ControllerBase
    {
        private readonly IProgramRunService _service;

        public ProgramRunController(IProgramRunService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{programRunId:Guid}")]
        public async Task<IActionResult> GetById(Guid programRunId)
        {
            var result = await _service.GetByIdAsync(programRunId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProgramRunDTO programRunDTO)
        {
            var result = await _service.CreateAsync(programRunDTO);
            return CreatedAtAction(nameof(GetById), new { programRunId = result.ProgramRunId }, result);
        }

        [HttpPut("{programRunId:Guid}")]
        public async Task<IActionResult> Update(Guid programRunId, [FromBody] ProgramRunDTO programRunDTO)
        {
            if (programRunId != programRunDTO.ProgramRunId) return BadRequest("ID mismatch.");
            var result = await _service.UpdateAsync(programRunDTO);
            return Ok(result);
        }

        [HttpDelete("{programRunId:Guid}")]
        public async Task<IActionResult> SoftDelete(Guid programRunId)
        {
            await _service.SoftDeleteAsync(programRunId);
            return NoContent();
        }
    }
}