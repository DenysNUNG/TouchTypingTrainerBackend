using Microsoft.AspNetCore.Mvc;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Services;

namespace TouchTypingTrainerBackend.Controllers
{
    /// <summary>
    /// Typing API controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TypingController : ControllerBase
    {
        /// <summary>
        /// Typing service.
        /// </summary>
        readonly private ITypingService _service;

        /// <summary>
        /// DI constructor.
        /// </summary>
        /// <param name="service">A typing service.</param>
        public TypingController(ITypingService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets all courses.
        /// </summary>
        [HttpGet("get-courses")]
        public async Task<IActionResult> GetCourses()
        {
            List<Course> courses = await _service.GetCourses();
            return Ok(courses);
        }

        /// <summary>
        /// Returns a course by id. The course includes lessons with exercises.
        /// </summary>
        /// <param name="courseId">A course id.</param>
        [HttpGet("get-course-with-lessons-and-exercises")]
        public async Task<IActionResult> GetCourseWithIncludes(int courseId)
        {
            Course course = await _service.GetCourseById(courseId,
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
            Course course = await _service.GetCourseById(courseId,
                includeLessonsWithExercises: false);
            return Ok(course);
        }
    }
}
