using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TouchTypingTrainerBackend.Services;

namespace TouchTypingTrainerBackend.Controllers
{
    /// <summary>
    /// User controller.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// User service.
        /// </summary>
        readonly private IUserService _userService;

        /// <summary>
        /// DI constructor.
        /// </summary>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets user-related learning results.
        /// </summary>
        /// <param name="courseId">Course identifier.</param>
        [HttpGet("get-learning-results")]
        public async Task<IActionResult> GetLearningResults(int courseId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var results = await _userService.GetUserLearningResultsAsync(userId, courseId);

            return Ok(results);
        }

        /// <summary>
        /// Gets user-related testing results.
        /// </summary>
        [HttpGet("get-testing-results")]
        public async Task<IActionResult> GetTestingResults()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var results = await _userService.GetUserTestingResultsAsync(userId);

            return Ok(results);
        }

        /// <summary>
        /// Gets current user exercise.
        /// </summary>
        /// <param name="courseId">Course identifier.</param>
        [Authorize]
        [HttpGet("get-current-exercise")]
        public async Task<IActionResult> GetCurrentUserExercise(int courseId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var exercise = await _userService.GetCurrentExercise(userId, courseId);

            return Ok(exercise);
        }
    }
}
