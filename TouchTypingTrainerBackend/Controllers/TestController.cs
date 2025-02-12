using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TouchTypingTrainerBackend.Services;

namespace TouchTypingTrainerBackend.Controllers
{
    /// <summary>
    /// Test API controller.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Test service.
        /// </summary>
        readonly private ITestService _ts;

        /// <summary>
        /// DI constructor.
        /// </summary>
        public TestController(ITestService testService)
        {
            _ts = testService;
        }

        /// <summary>
        /// Gets random testing material set.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("get-random-test-set")]
        public async Task<IActionResult> GetRandomTestingMaterial()
        {
            var randomTestSet = await _ts.GetRandomTestingMaterial();

            return Ok(randomTestSet);
        }

        /// <summary>
        /// Gets user-related testing results.
        /// </summary>
        [HttpGet("get-testing-results")]
        public async Task<IActionResult> GetTestingResults()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var results = await _ts.GetUserTestingResultsAsync(userId);

            return Ok(results);
        }
    }
}
