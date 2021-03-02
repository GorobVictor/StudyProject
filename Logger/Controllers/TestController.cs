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
            _logger.LogDebug("api/test debug");
            try
            {
                int x = 0;
                _logger.LogWarning("api/test x=0");
                var a = 5 / x;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "api/test Error");
                _logger.LogCritical(ex, "api/test Critical");
            }
            _logger.LogInformation("api/test Information");
            return Ok(new
            {
                Name = "test",
                Age = 22
            });
        }
    }
}
