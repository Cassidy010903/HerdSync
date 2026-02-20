using BLL.Services;
using HerdSync.Shared.DTO.Program;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Program.ProgramTemplate
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramTemplateRuleController : ControllerBase
    {
        private readonly IProgramTemplateRuleService _service;
        public ProgramTemplateRuleController(IProgramTemplateRuleService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{programTemplateRuleId:Guid}")]
        public async Task<IActionResult> GetById(Guid programTemplateRuleId)
        {
            var result = await _service.GetByIdAsync(programTemplateRuleId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProgramTemplateRuleDTO programTemplateRuleDTO)
        {
            var result = await _service.CreateAsync(programTemplateRuleDTO);
            return CreatedAtAction(nameof(GetById), new { programTemplateRuleId = result.Id }, result);
        }

        [HttpPut("{programTemplateRuleId:Guid}")]
        public async Task<IActionResult> Update(Guid programTemplateRuleId, [FromBody] ProgramTemplateRuleDTO programTemplateRuleDTO)
        {
            if (programTemplateRuleId != programTemplateRuleDTO.ProgramTemplateRuleId) return BadRequest("ID mismatch.");
            var result = await _service.UpdateAsync(programTemplateRuleDTO);
            return Ok(result);
        }

        [HttpDelete("{programTemplateRuleId:Guid}")]
        public async Task<IActionResult> SoftDelete(Guid programTemplateRuleId)
        {
            await _service.SoftDeleteAsync(programTemplateRuleId);
            return NoContent();
        }
    }
}