using TouchTypingTrainerBackend.Entities;

namespace TouchTypingTrainerBackend.Services
{
    /// <summary>
    /// Course entity service.
    /// </summary>
    public interface ITypingService
    {
        /// <summary>
        /// Gets all courses.
        /// </summary>
        Task<List<Course>> GetCourses();

        /// <summary>
        /// Gets course by id.
        /// </summary>
        /// <param name="id">An identifier</param>
        /// <param name="includeLessonsWithExercises">A flag that indicates whether
        /// include lessons with exercises or not.</param>
        Task<Course> GetCourseById(int id, bool includeLessonsWithExercises);
    }
}
