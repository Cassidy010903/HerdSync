using BLL.Services;
using HerdSync.Shared.DTO.Program;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Program.ProgramRun
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramRunAnimalController : ControllerBase
    {
        private readonly IProgramRunAnimalService _service;

        public ProgramRunAnimalController(IProgramRunAnimalService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{programRunAnimalId:Guid}")]
        public async Task<IActionResult> GetById(Guid programRunAnimalId)
        {
            var result = await _service.GetByIdAsync(programRunAnimalId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProgramRunAnimalDTO programRunAnimalDTO)
        {
            var result = await _service.CreateAsync(programRunAnimalDTO);
            return CreatedAtAction(nameof(GetById), new { programRunAnimalId = result.ProgramRunAnimalId }, result);
        }

        [HttpPut("{programRunAnimalId:Guid}")]
        public async Task<IActionResult> Update(Guid programRunAnimalId, [FromBody] ProgramRunAnimalDTO programRunAnimalDTO)
        {
            if (programRunAnimalId != programRunAnimalDTO.ProgramRunAnimalId) return BadRequest("programRunAnimalId mismatch.");
            var result = await _service.UpdateAsync(programRunAnimalDTO);
            return Ok(result);
        }

        [HttpDelete("{programRunAnimalId:Guid}")]
        public async Task<IActionResult> SoftDelete(Guid programRunAnimalId)
        {
            await _service.SoftDeleteAsync(programRunAnimalId);
            return NoContent();
        }
    }
}