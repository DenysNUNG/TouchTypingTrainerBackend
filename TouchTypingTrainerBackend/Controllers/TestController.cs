using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Models;
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
        public async Task<TestingMaterial> GetRandomTestingMaterial()
        {
            return await _testService.GetRandomTestingMaterialAsync();
        }

        /// <summary>
        /// Gets user-related testing results.
        /// </summary>
        [HttpGet("get-testing-results")]
        public async Task<List<TestingResult>> GetTestingResults()
        {
            string userId = _userService.GetUserId();
            var results = await _testService.GetUserTestingResultsAsync(userId);

            return results;
        }

        /// <summary>
        /// Completes typing test.
        /// </summary>
        /// <param name="material">Testing material.</param>
        /// <param name="mistakesCount">Count of mistakes.</param>
        /// <param name="duration">Typing duration.</param>
        [AllowAnonymous]
        [HttpPost("complete")]
        public async Task<IUserResult> CompleteTest([FromBody]TestCompleteRequest request)
        {
            var result = _calcService.CalculatePerformance<TestingResult>(
                request.TestingMaterial.Text,
                request.MistakesCount,
                request.Duration);

            string userId = _userService.GetUserId();

            if (userId is not null)
            {
                await _testService.AddUserTestingResultAsync(userId,
                    request.TestingMaterial.Id, result);
            }

            return result;
        }
    }
}
