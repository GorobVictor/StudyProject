using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private ILogger _logger { get; }
        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("api/test OK");
            return Ok(new
            {
                Name = "test",
                Age = 22
            });
        }
    }
}
