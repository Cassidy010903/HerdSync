using BLL.Services;
using HerdSync.Shared.DTO.Farm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Farm
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FarmActivityController : ControllerBase
    {
        private readonly IFarmActivityService _service;
        public FarmActivityController(IFarmActivityService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(Guid farmActivityId)
        {
            var result = await _service.GetByIdAsync(farmActivityId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FarmActivityDTO farmActivityDTO)
        {
            var result = await _service.CreateAsync(farmActivityDTO);
            return CreatedAtAction(nameof(GetById), new { id = result.FarmActivityId }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(Guid farmActivityId, [FromBody] FarmActivityDTO farmActivityDTO)
        {
            if (farmActivityId != farmActivityDTO.FarmActivityId) return BadRequest("ID mismatch.");
            var result = await _service.UpdateAsync(farmActivityDTO);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> SoftDelete(Guid farmActivityId)
        {
            await _service.SoftDeleteAsync(farmActivityId);
            return NoContent();
        }
    }
}