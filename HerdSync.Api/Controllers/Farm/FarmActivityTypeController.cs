using BLL.Services;
using HerdSync.Shared.DTO.Farm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Farm
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FarmActivityTypeController : ControllerBase
    {
        private readonly IFarmActivityTypeService _service;
        public FarmActivityTypeController(IFarmActivityTypeService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(string code)
        {
            var result = await _service.GetByIdAsync(code);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FarmActivityTypeDTO farmActivityTypeDTO)
        {
            var result = await _service.CreateAsync(farmActivityTypeDTO);
            return CreatedAtAction(nameof(GetById), new { id = result.FarmActivityTypeCode }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(string code, [FromBody] FarmActivityTypeDTO farmActivityTypeDTO)
        {
            if (code != farmActivityTypeDTO.FarmActivityTypeCode) return BadRequest("Code mismatch.");
            var result = await _service.UpdateAsync(farmActivityTypeDTO);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> SoftDelete(string code)
        {
            await _service.SoftDeleteAsync(code);
            return NoContent();
        }
    }
}