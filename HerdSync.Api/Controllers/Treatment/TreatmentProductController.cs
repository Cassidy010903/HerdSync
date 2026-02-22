using BLL.Services;
using HerdSync.Shared.DTO.Treatment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Treatment
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TreatmentProductController : ControllerBase
    {
        private readonly ITreatmentProductService _service;
        public TreatmentProductController(ITreatmentProductService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{treatmentProductId:Guid}")]
        public async Task<IActionResult> GetById(Guid treatmentProductId)
        {
            var result = await _service.GetByIdAsync(treatmentProductId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TreatmentProductDTO treatmentProductDTO)
        {
            var result = await _service.CreateAsync(treatmentProductDTO);
            return CreatedAtAction(nameof(GetById), new { treatmentProductId = result.TreatmentProductId }, result);
        }

        [HttpPut("{treatmentProductId:Guid}")]
        public async Task<IActionResult> Update(Guid treatmentProductId, [FromBody] TreatmentProductDTO treatmentProductDTO)
        {
            if (treatmentProductId != treatmentProductDTO.TreatmentProductId) return BadRequest("treatmentProductId mismatch.");
            var result = await _service.UpdateAsync(treatmentProductDTO);
            return Ok(result);
        }

        [HttpDelete("{treatmentProductId:Guid}")]
        public async Task<IActionResult> SoftDelete(Guid treatmentProductId)
        {
            await _service.SoftDeleteAsync(treatmentProductId);
            return NoContent();
        }
    }
}