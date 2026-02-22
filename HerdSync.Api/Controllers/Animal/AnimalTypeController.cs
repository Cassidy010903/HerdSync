using BLL.Services;
using HerdSync.Shared.DTO.Animal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Animal
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalTypeController : ControllerBase
    {
        private readonly IAnimalTypeService _service;

        public AnimalTypeController(IAnimalTypeService service) => _service = service;

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
        public async Task<IActionResult> Create([FromBody] AnimalTypeDTO animalTypeDTO)
        {
            var result = await _service.CreateAsync(animalTypeDTO);
            return CreatedAtAction(nameof(GetById), new { id = result.AnimalTypeCode }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(string code, [FromBody] AnimalTypeDTO animalTypeDTO)
        {
            if (code != animalTypeDTO.AnimalTypeCode) return BadRequest("Code mismatch.");
            var result = await _service.UpdateAsync(animalTypeDTO);
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