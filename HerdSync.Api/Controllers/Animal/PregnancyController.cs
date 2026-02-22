using BLL.Services;
using HerdSync.Shared.DTO.Animal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Animal
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PregnancyController : ControllerBase
    {
        private readonly IPregnancyService _service;

        public PregnancyController(IPregnancyService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(Guid pregnancyId)
        {
            var result = await _service.GetByIdAsync(pregnancyId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PregnancyDTO pregnancyDTO)
        {
            var result = await _service.CreateAsync(pregnancyDTO);
            return CreatedAtAction(nameof(GetById), new { id = result.PregnancyId }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(Guid pregnancyId, [FromBody] PregnancyDTO pregnancyDTO)
        {
            if (pregnancyId != pregnancyDTO.PregnancyId) return BadRequest("ID mismatch.");
            var result = await _service.UpdateAsync(pregnancyDTO);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> SoftDelete(Guid pregnancyId)
        {
            await _service.SoftDeleteAsync(pregnancyId);
            return NoContent();
        }
    }
}