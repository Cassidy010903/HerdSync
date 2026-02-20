using BLL.Services;
using HerdSync.Shared.DTO.Farm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Farm
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FarmController : ControllerBase
    {
        private readonly IFarmService _service;
        public FarmController(IFarmService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(Guid farmId)
        {
            var result = await _service.GetByIdAsync(farmId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FarmDTO farmDTO)
        {
            var result = await _service.CreateAsync(farmDTO);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(Guid farmId, [FromBody] FarmDTO farmDTO)
        {
            if (farmId != farmDTO.FarmId) return BadRequest("ID mismatch.");
            var result = await _service.UpdateAsync(farmDTO);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> SoftDelete(Guid farmId)
        {
            await _service.SoftDeleteAsync(farmId);
            return NoContent();
        }
    }
}