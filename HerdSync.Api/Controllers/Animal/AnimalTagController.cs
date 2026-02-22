using BLL.Services;
using HerdSync.Shared.DTO.Animal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Animal
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalTagController : ControllerBase
    {
        private readonly IAnimalTagService _service;
        public AnimalTagController(IAnimalTagService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(Guid animalTagId)
        {
            var result = await _service.GetByIdAsync(animalTagId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AnimalTagDTO animalTagDTO)
        {
            var result = await _service.CreateAsync(animalTagDTO);
            return CreatedAtAction(nameof(GetById), new { id = result.AnimalId }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(Guid animalTagId, [FromBody] AnimalTagDTO animalTagDTO)
        {
            if (animalTagId != animalTagDTO.AnimalTagId) return BadRequest("ID mismatch.");
            var result = await _service.UpdateAsync(animalTagDTO);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> SoftDelete(Guid animalTagId)
        {
            await _service.SoftDeleteAsync(animalTagId);
            return NoContent();
        }
    }
}