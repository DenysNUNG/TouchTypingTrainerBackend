using Microsoft.AspNetCore.Mvc;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Repositories;
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
        ///
        /// </summary>
        readonly private ITypingService _service;

        public TypingController(ITypingService service)
        {
            _service = service;
        }

        [HttpGet("get-courses")]
        public async Task<IActionResult> GetCourses()
        {
            List<Course> courses = await _service.GetCourses();
            return Ok(courses);
        }

        [HttpGet("get-course-with-lessons-and-exercises")]
        public async Task<IActionResult> GetCourse(int courseId)
        {
            Course course = await _service.GetCourseById(courseId,
                includeLessonsWithExercises: true);
            return Ok(course);
        }
    }
}
