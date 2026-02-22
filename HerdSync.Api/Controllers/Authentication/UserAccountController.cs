using BLL.Services;
using HerdSync.Shared.DTO.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HerdSync.Api.Controllers.Authentication
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountService _service;

        public UserAccountController(IUserAccountService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(Guid userAccountId)
        {
            var result = await _service.GetByIdAsync(userAccountId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserAccountDTO userAccountDTO)
        {
            var result = await _service.CreateAsync(userAccountDTO);
            return CreatedAtAction(nameof(GetById), new { id = result.UserId }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(Guid userAccountId, [FromBody] UserAccountDTO userAccountDTO)
        {
            if (userAccountId != userAccountDTO.UserId) return BadRequest("ID mismatch.");
            var result = await _service.UpdateAsync(userAccountDTO);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> SoftDelete(Guid userAccountId)
        {
            await _service.SoftDeleteAsync(userAccountId);
            return NoContent();
        }
    }
}