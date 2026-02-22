using BLL.Services;
using HerdSync.Shared.DTO.Treatment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Treatment
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TreatmentController : ControllerBase
    {
        private readonly ITreatmentService _service;
        public TreatmentController(ITreatmentService service) => _service = service;

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
        public async Task<IActionResult> Create([FromBody] TreatmentDTO treatmentDTO)
        {
            var result = await _service.CreateAsync(treatmentDTO);
            return CreatedAtAction(nameof(GetById), new { code = result.TreatmentCode }, result);
        }

        [HttpPut("{code:string}")]
        public async Task<IActionResult> Update(string code, [FromBody] TreatmentDTO treatmentDTO)
        {
            if (code != treatmentDTO.TreatmentCode) return BadRequest("code mismatch.");
            var result = await _service.UpdateAsync(treatmentDTO);
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