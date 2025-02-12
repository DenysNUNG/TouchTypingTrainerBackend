using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TouchTypingTrainerBackend.Services;

namespace TouchTypingTrainerBackend.Controllers
{
    /// <summary>
    /// User API controller.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// User service.
        /// </summary>
        readonly private IUserService _us;

        /// <summary>
        /// DI constructor.
        /// </summary>
        public UserController(IUserService userService)
        {
            _us = userService;
        }

        /// <summary>
        /// Gets user-related learning results.
        /// </summary>
        /// <param name="courseId">Course identifier.</param>
        [HttpGet("get-learning-results")]
        public async Task<IActionResult> GetLearningResults(int courseId)
        {
            string userId = GetUserId();
            var results = await _us.GetUserLearningResultsAsync(userId, courseId);

            return Ok(results);
        }

        /// <summary>
        /// Gets user-related testing results.
        /// </summary>
        [HttpGet("get-testing-results")]
        public async Task<IActionResult> GetTestingResults()
        {
            string userId = GetUserId();
            var results = await _us.GetUserTestingResultsAsync(userId);

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
            string userId = GetUserId();
            var exercise = await _us.GetCurrentExercise(userId, courseId);

            return Ok(exercise);
        }

        /// <summary>
        /// Gets current user identifier.
        /// </summary>
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
