using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Models;
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
        public async Task<List<Course>> GetCourses()
        {
            return await _tutorService.GetCoursesAsync();
        }

        /// <summary>
        /// Returns a course by id. The course includes lessons with exercises.
        /// </summary>
        /// <param name="courseId">A course identifier.</param>
        [HttpGet("get-course-with-lessons-and-exercises")]
        public async Task<Course> GetCourseWithIncludes(int courseId)
        {
            Course course = await _tutorService.GetCourseByIdAsync(courseId,
                includeLessonsWithExercises: true);

            return course;
        }

        /// <summary>
        /// Returns a course by id.
        /// </summary>
        /// <param name="courseId">A course identifier.</param>
        [HttpGet("get-course")]
        public async Task<Course> GetCourse(int courseId)
        {
            Course course = await _tutorService.GetCourseByIdAsync(courseId,
                includeLessonsWithExercises: false);

            return course;
        }

        /// <summary>
        /// Gets user-related learning results.
        /// </summary>
        /// <param name="courseId">Course identifier.</param>
        [HttpGet("get-learning-results")]
        public async Task<List<LearningResult>> GetLearningResults(int courseId)
        {
            string userId = _userService.GetUserId();
            var results = await _tutorService.GetUserLearningResultsAsync(userId, courseId);

            return results;
        }

        /// <summary>
        /// Gets current user exercise
        /// </summary>
        /// <param name="courseId">Course identifier.</param>
        /// <param name="exerciseId">Exercise identifier.</param>
        [HttpGet("get-current-exercise")]
        public async Task<IActionResult> GetCurrentUserExercise(int courseId, int? exerciseId)
        {
            string userId = _userService.GetUserId();
            var exercise = await _tutorService.GetCurrentExerciseAsync(userId, courseId, exerciseId);

            return exercise is not null? Ok(exercise) : NotFound();
        }

        /// <summary>
        /// Completes typing exercise.
        /// </summary>
        /// <param name="request">Exercise complete request.</param>
        [HttpPost("complete-exercise")]
        public async Task<IUserResult> CompleteExercise([FromBody] ExerciseCompleteRequest request)
        {
            var result = _calcService.CalculatePerformance<LearningResult>(
                request.Exercise.StudySet,
                request.MistakesCount,
                request.Duration);

            result.ExerciseId = request.Exercise.Id;

            string userId = _userService.GetUserId();

            await _tutorService.AddUserLearningResultAsync(userId, result);
            await _tutorService.UpsertUserCourseProgressAsync(userId, request.CourseId);

            return result;
        }

        /// <summary>
        /// Registers new user-course.
        /// </summary>
        /// <param name="courseId">Course identifier.</param>
        [HttpPost("register-user-course")]
        public async Task<IActionResult> RegisterUserCourse(int courseId)
        {
            string userId = _userService.GetUserId();
            await _tutorService.UpsertUserCourseProgressAsync(userId, courseId);

            return Ok();
        }

        /// <summary>
        /// Gets user-related courses.
        /// </summary>
        [HttpGet("get-user-courses")]
        public async Task<List<Course>> GetUserCourses()
        {
            string userId = _userService.GetUserId();
            var courses = await _tutorService.GetUserCoursesAsync(userId);

            return courses;
        }

        /// <summary>
        /// Gets related layout keys for course.
        /// </summary>
        /// <param name="courseId">Course identifier</param>
        [HttpGet("get-course-layout-keys")]
        public async Task<List<LayoutKey>> GetCourseLayoutKeys(int courseId)
        {
            return await _tutorService.GetCourseLayoutKeysAsync(courseId);
        }

        /// <summary>
        /// Gets all layouts.
        /// </summary>
        [HttpGet("get-layouts")]
        public async Task<List<Layout>> GetAllLayouts()
        {
            return await _tutorService.GetAllLayoutsAsync();
        }
    }
}
