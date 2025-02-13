using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Services;

namespace TouchTypingTrainerBackend.Controllers
{
    /// <summary>
    /// Tutorial API controller.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TutorialController : ControllerBase
    {
        /// <summary>
        /// Tutorial service.
        /// </summary>
        readonly private ITutorialService _tutorService;

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
        public TutorialController(ITutorialService tutorialService,
            ICalcService calcService,
            IUserService userService)
        {
            _tutorService = tutorialService;
            _calcService = calcService;
            _userService = userService;
        }

        /// <summary>
        /// Gets all courses.
        /// </summary>
        [HttpGet("get-courses")]
        public async Task<IActionResult> GetCourses()
        {
            List<Course> courses = await _tutorService.GetCoursesAsync();

            return Ok(courses);
        }

        /// <summary>
        /// Returns a course by id. The course includes lessons with exercises.
        /// </summary>
        /// <param name="courseId">A course identifier.</param>
        [HttpGet("get-course-with-lessons-and-exercises")]
        public async Task<IActionResult> GetCourseWithIncludes(int courseId)
        {
            Course course = await _tutorService.GetCourseByIdAsync(courseId,
                includeLessonsWithExercises: true);

            return Ok(course);
        }

        /// <summary>
        /// Returns a course by id.
        /// </summary>
        /// <param name="courseId">A course identifier.</param>
        [HttpGet("get-course")]
        public async Task<IActionResult> GetCourse(int courseId)
        {
            Course course = await _tutorService.GetCourseByIdAsync(courseId,
                includeLessonsWithExercises: false);

            return Ok(course);
        }

        /// <summary>
        /// Gets user-related learning results.
        /// </summary>
        /// <param name="courseId">Course identifier.</param>
        [HttpGet("get-learning-results")]
        public async Task<IActionResult> GetLearningResults(int courseId)
        {
            string userId = _userService.GetUserId();
            var results = await _tutorService.GetUserLearningResultsAsync(userId, courseId);

            return Ok(results);
        }

        /// <summary>
        /// Gets current user exercise.
        /// </summary>
        /// <param name="courseId">Course identifier.</param>
        [HttpGet("get-current-exercise")]
        public async Task<IActionResult> GetCurrentUserExercise(int courseId)
        {
            string userId = _userService.GetUserId();
            var exercise = await _tutorService.GetCurrentExercise(userId, courseId);

            return Ok(exercise);
        }

        /// <summary>
        /// Completes typing exercise.
        /// </summary>
        /// <param name="exercise">Exercise.</param>
        /// <param name="mistakesCount">Count of mistakes.</param>
        /// <param name="duration">Typing duration.</param>
        [HttpPost("complete-exercise")]
        public async Task<IActionResult> CompleteExercise(Exercise exercise,
            int courseId,
            int mistakesCount,
            int duration)
        {
            var result = _calcService.CalculatePerformance<LearningResult>(exercise.StudySet,
                mistakesCount,
                duration);
            
            result.ExerciseId = exercise.Id;

            string userId = _userService.GetUserId();

            await _tutorService.AddUserLearningResultAsync(userId, result);
            await _tutorService.UpsertUserCourseProgress(userId, courseId);

            return Ok(result);
        }

        /// <summary>
        /// Registers new user-course.
        /// </summary>
        /// <param name="courseId">Course identifier.</param>
        [HttpPost("register-user-course")]
        public async Task<IActionResult> RegisterUserCourse(int courseId)
        {
            string userId = _userService.GetUserId();
            await _tutorService.UpsertUserCourseProgress(userId, courseId);

            return Ok();
        }

        /// <summary>
        /// Gets user-related courses.
        /// </summary>
        [HttpGet("get-user-courses")]
        public async Task<IActionResult> GetUserCourses()
        {
            string userId = _userService.GetUserId();
            var courses = await _tutorService.GetUserCourses(userId);

            return Ok(courses);
        }
    }
}
