using BLL.Services;
using HerdSync.Shared.DTO.Program;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Program.ProgramTemplate
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramTemplateRuleTreatmentController : ControllerBase
    {
        private readonly IProgramTemplateRuleTreatmentService _service;
        public ProgramTemplateRuleTreatmentController(IProgramTemplateRuleTreatmentService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{programTemplateRuleTreatmentId:Guid}")]
        public async Task<IActionResult> GetById(Guid programTemplateRuleTreatmentId)
        {
            var result = await _service.GetByIdAsync(programTemplateRuleTreatmentId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProgramTemplateRuleTreatmentDTO programTemplateRuleTreatmentDTO)
        {
            var result = await _service.CreateAsync(programTemplateRuleTreatmentDTO);
            return CreatedAtAction(nameof(GetById), new { programTemplateRuleTreatmentId = result.ProgramTemplateRuleTreatmentId }, result);
        }

        [HttpPut("{programTemplateRuleTreatmentId:Guid}")]
        public async Task<IActionResult> Update(Guid programTemplateRuleTreatmentId, [FromBody] ProgramTemplateRuleTreatmentDTO programTemplateRuleTreatmentDTO)
        {
            if (programTemplateRuleTreatmentId != programTemplateRuleTreatmentDTO.ProgramTemplateRuleTreatmentId) return BadRequest("ID mismatch.");
            var result = await _service.UpdateAsync(programTemplateRuleTreatmentDTO);
            return Ok(result);
        }

        [HttpDelete("{programTemplateRuleTreatmentId:Guid}")]
        public async Task<IActionResult> SoftDelete(Guid programTemplateRuleTreatmentId)
        {
            await _service.SoftDeleteAsync(programTemplateRuleTreatmentId);
            return NoContent();
        }
    }
}