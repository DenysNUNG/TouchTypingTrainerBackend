using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TouchTypingTrainerBackend.Entities;
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
        readonly private ITestService _testService;

        /// <summary>
        /// User performance calculation service.
        /// </summary>
        readonly private ICalcService _calcService;

        /// <summary>
        /// HttpContext user service.
        /// </summary>
        readonly private IUserService _userService;

        /// <summary>
        /// DI constructor.
        /// </summary>
        public TestController(ITestService testService,
            ICalcService calcService,
            IUserService userService)
        {
            _testService = testService;
            _calcService = calcService;
            _userService = userService;
        }

        /// <summary>
        /// Gets random testing material set.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("get-random-test-set")]
        public async Task<IActionResult> GetRandomTestingMaterial()
        {
            var randomTestSet = await _testService.GetRandomTestingMaterialAsync();

            return Ok(randomTestSet);
        }

        /// <summary>
        /// Gets user-related testing results.
        /// </summary>
        [HttpGet("get-testing-results")]
        public async Task<IActionResult> GetTestingResults()
        {
            string userId = _userService.GetUserId();
            var results = await _testService.GetUserTestingResultsAsync(userId);

            return Ok(results);
        }

        /// <summary>
        /// Completes typing test.
        /// </summary>
        /// <param name="material">Testing material.</param>
        /// <param name="mistakesCount">Count of mistakes.</param>
        /// <param name="duration">Typing duration.</param>
        [AllowAnonymous]
        [HttpPost("complete")]
        public async Task<IActionResult> CompleteTest(TestingMaterial material,
            int mistakesCount,
            int duration)
        {
            var result = _calcService.CalculatePerformance<TestingResult>(material.Text,
                mistakesCount,
                duration);

            string userId = _userService.GetUserId();

            if (userId is not null)
            {
                await _testService.AddUserTestingResultAsync(userId, material.Id, result);
            }

            return Ok(result);
        }
    }
}
