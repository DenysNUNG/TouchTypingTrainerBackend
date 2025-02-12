using TouchTypingTrainerBackend.Entities;

namespace TouchTypingTrainerBackend.Services
{
    /// <summary>
    /// Typing tutorial service.
    /// </summary>
    public interface ITutorialService
    {
        /// <summary>
        /// Gets all courses.
        /// </summary>
        Task<List<Course>> GetCoursesAsync();

        /// <summary>
        /// Gets a course by id.
        /// </summary>
        /// <param name="id">An identifier</param>
        /// <param name="includeLessonsWithExercises">A flag that indicates whether
        /// include lessons with exercises or not.</param>
        Task<Course> GetCourseByIdAsync(int id, bool includeLessonsWithExercises);

        /// <summary>
        /// Gets learning results for user-course.
        /// </summary>
        /// <param name="userId">A user identifier.</param>
        /// <param name="courseId">A Course identifier.</param>
        Task<List<LearningResult>> GetUserLearningResultsAsync(string userId, int courseId);

        /// <summary>
        /// Gets current user-related exercise.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="courseId">Course idetifier.</param>
        Task<Exercise> GetCurrentExercise(string userId, int courseId);
    }
}
