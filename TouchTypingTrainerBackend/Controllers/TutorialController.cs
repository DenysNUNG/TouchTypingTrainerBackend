using Microsoft.AspNetCore.Mvc;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Services;

namespace TouchTypingTrainerBackend.Controllers
{
    /// <summary>
    /// Tutorial API controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TutorialController : ControllerBase
    {
        /// <summary>
        /// Tutorial service.
        /// </summary>
        readonly private ITutorialService _service;

        /// <summary>
        /// DI constructor.
        /// </summary>
        /// <param name="service">A tutorial service.</param>
        public TutorialController(ITutorialService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets all courses.
        /// </summary>
        [HttpGet("get-courses")]
        public async Task<IActionResult> GetCourses()
        {
            List<Course> courses = await _service.GetCoursesAsync();
            return Ok(courses);
        }

        /// <summary>
        /// Returns a course by id. The course includes lessons with exercises.
        /// </summary>
        /// <param name="courseId">A course id.</param>
        [HttpGet("get-course-with-lessons-and-exercises")]
        public async Task<IActionResult> GetCourseWithIncludes(int courseId)
        {
            Course course = await _service.GetCourseByIdAsync(courseId,
                includeLessonsWithExercises: true);
            return Ok(course);
        }

        /// <summary>
        /// Returns a course by id.
        /// </summary>
        /// <param name="courseId">A course id.</param>
        [HttpGet("get-course")]
        public async Task<IActionResult> GetCourse(int courseId)
        {
            Course course = await _service.GetCourseByIdAsync(courseId,
                includeLessonsWithExercises: false);
            return Ok(course);
        }
    }
}
