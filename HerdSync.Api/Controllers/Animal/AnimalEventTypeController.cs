using BLL.Services;
using HerdSync.Shared.DTO.Animal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Animal
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalEventTypeController : ControllerBase
    {
        private readonly IAnimalEventTypeService _service;
        public AnimalEventTypeController(IAnimalEventTypeService service) => _service = service;

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
        public async Task<IActionResult> Create([FromBody] AnimalEventTypeDTO animalEventTypeDTO)
        {
            var result = await _service.CreateAsync(animalEventTypeDTO);
            return CreatedAtAction(nameof(GetById), new { id = result.EventTypeCode }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(string code, [FromBody] AnimalEventTypeDTO animalEventTypeDTO)
        {
            if (code != animalEventTypeDTO.EventTypeCode) return BadRequest("Code mismatch.");
            var result = await _service.UpdateAsync(animalEventTypeDTO);
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