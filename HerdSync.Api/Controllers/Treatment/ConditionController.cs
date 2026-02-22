using BLL.Services;
using HerdSync.Shared.DTO.Treatment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Treatment
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ConditionController : ControllerBase
    {
        private readonly IConditionService _service;
        public ConditionController(IConditionService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{code:string}")]
        public async Task<IActionResult> GetBycode(string code)
        {
            var result = await _service.GetBycodeAsync(code);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConditionDTO conditionDTO)
        {
            var result = await _service.CreateAsync(conditionDTO);
            return CreatedAtAction(nameof(GetBycode), new { code = result.ConditionCode }, result);
        }

        [HttpPut("{code:string}")]
        public async Task<IActionResult> Update(string code, [FromBody] ConditionDTO conditionDTO)
        {
            if (code != conditionDTO.ConditionCode) return BadRequest("code mismatch.");
            var result = await _service.UpdateAsync(conditionDTO);
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