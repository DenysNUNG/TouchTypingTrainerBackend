using Microsoft.AspNetCore.Mvc;
using TouchTypingTrainerBackend.Services;

namespace TouchTypingTrainerBackend.Controllers
{
    /// <summary>
    /// Test API controller.
    /// </summary>
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
        [HttpGet("get-random-test-set")]
        public async Task<IActionResult> GetRandomTestingMaterial()
        {
            var testSet = await _ts.GetRandomTestingMaterial();
            return Ok(testSet);
        }
    }
}
