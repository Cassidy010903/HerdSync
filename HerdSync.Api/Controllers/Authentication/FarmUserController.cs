using BLL.Services;
using HerdSync.Shared.DTO.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Authentication
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FarmUserController : ControllerBase
    {
        private readonly IFarmUserService _service;
        public FarmUserController(IFarmUserService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(Guid farmUserId)
        {
            var result = await _service.GetByIdAsync(farmUserId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FarmUserDTO farmUserDTO)
        {
            var result = await _service.CreateAsync(farmUserDTO);
            return CreatedAtAction(nameof(GetById), new { id = result.FarmUserId }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] FarmUserDTO farmUserDTO)
        {
            if (id != farmUserDTO.UserId) return BadRequest("ID mismatch.");
            var result = await _service.UpdateAsync(farmUserDTO);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> SoftDelete(Guid farmUserId)
        {
            await _service.SoftDeleteAsync(farmUserId);
            return NoContent();
        }
    }

}