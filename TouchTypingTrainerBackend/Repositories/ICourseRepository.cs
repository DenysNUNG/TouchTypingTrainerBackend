using TouchTypingTrainerBackend.Entities;

namespace TouchTypingTrainerBackend.Repositories
{
    /// <summary>
    /// Course entity repository.
    /// </summary>
    public interface ICourseRepository
    {
        /// <summary>
        /// Gets all courses.
        /// </summary>
        Task<List<Course>> GetCoursesAsync();

        /// <summary>
        /// Gets course by id.
        /// </summary>
        /// <param name="id">An identifier</param>
        /// <param name="includeLessonsWithExercises">A flag that indicates whether
        /// include lessons with exercises or not.</param>
        Task<Course> GetCourseByIdAsync(int id, bool includeLessonsWithExercises);
    }
}
