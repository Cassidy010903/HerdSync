using BLL.Services;
using HerdSync.Shared.DTO.Program;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Program.ProgramTemplate
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramTemplateController : ControllerBase
    {
        private readonly IProgramTemplateService _service;
        public ProgramTemplateController(IProgramTemplateService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{code:string}")]
        public async Task<IActionResult> GetById(string code)
        {
            var result = await _service.GetByIdAsync(code);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProgramTemplateDTO programTemplateDTO)
        {
            var result = await _service.CreateAsync(programTemplateDTO);
            return CreatedAtAction(nameof(GetById), new { code = result.Id }, result);
        }

        [HttpPut("{code:string}")]
        public async Task<IActionResult> Update(string code, [FromBody] ProgramTemplateDTO programTemplateDTO)
        {
            if (code != programTemplateDTO.ProgramTemplateCode) return BadRequest("ID mismatch.");
            var result = await _service.UpdateAsync(programTemplateDTO);
            return Ok(result);
        }

        [HttpDelete("{code:string}")]
        public async Task<IActionResult> SoftDelete(string code)
        {
            await _service.SoftDeleteAsync(code);
            return NoContent();
        }
    }
}