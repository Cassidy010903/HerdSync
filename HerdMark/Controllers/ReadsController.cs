using HerdMark.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HerdMark.Models;

namespace HerdMark.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadsController : ControllerBase
    {
        private readonly ReadMappingService _service;
        private readonly ILogger<ReadsController> _logger;

        public ReadsController(ReadMappingService service, ILogger<ReadsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HerdMarkRead[] reads)
        {
            if (reads == null || reads.Length == 0)
                return BadRequest(new { error = "No reads received" });

            foreach (var r in reads)
            {
                if (r.TimestampUtc == default) r.TimestampUtc = DateTime.UtcNow;
                await _service.AddReadAsync(r);
            }

            return Ok(new { accepted = reads.Length });
        }
    }
}
