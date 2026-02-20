using BLL.Services;
using HerdSync.Shared.DTO.Treatment;
using HerdSync.Shared.treatmentCategoryDTO.Treatment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Treatment
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TreatmentCategoryController : ControllerBase
    {
        private readonly ITreatmentCategoryService _service;
        public TreatmentCategoryController(ITreatmentCategoryService service) => _service = service;

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
        public async Task<IActionResult> Create([FromBody] TreatmentCategoryDTO treatmentCategoryDTO)
        {
            var result = await _service.CreateAsync(treatmentCategoryDTO);
            return CreatedAtAction(nameof(GetById), new { code = result.code }, result);
        }

        [HttpPut("{code:string}")]
        public async Task<IActionResult> Update(string code, [FromBody] TreatmentCategoryDTO treatmentCategoryDTO)
        {
            if (code != treatmentCategoryDTO.CategoryName) return BadRequest("Name mismatch.");
            var result = await _service.UpdateAsync(treatmentCategoryDTO);
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