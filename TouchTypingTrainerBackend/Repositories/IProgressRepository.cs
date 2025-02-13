using TouchTypingTrainerBackend.Entities;

namespace TouchTypingTrainerBackend.Repositories
{
    /// <summary>
    /// UserCourseProgress entity repository.
    /// </summary>
    public interface IProgressRepository
    {
        /// <summary>
        /// Gets current user-related exercise.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="courseId">Course idetifier.</param>
        Task<Exercise> GetCurrentExerciseAsync(string userId, int courseId);

        /// <summary>
        /// Registers user-course.
        /// </summary>
        /// <param name="userId">User idetifier.</param>
        /// <param name="courseId">Course idetifier.</param>
        Task UpsertUserCourseProgressAsync(string userId, int courseId);

        /// <summary>
        /// Gets user-related courses.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        Task<List<Course>> GetUserCoursesAsync(string userId);
    }
}
