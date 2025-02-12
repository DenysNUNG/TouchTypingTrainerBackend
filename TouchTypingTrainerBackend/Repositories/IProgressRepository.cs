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
        Task<Exercise> GetCurrentExercise(string userId, int courseId);

        /// <summary>
        /// Registers user-course.
        /// </summary>
        /// <param name="userId">User idetifier.</param>
        /// <param name="courseId">Course idetifier.</param>
        Task UpsertUserCourseProgress(string userId, int courseId);
    }
}
