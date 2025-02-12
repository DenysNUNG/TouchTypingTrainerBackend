using TouchTypingTrainerBackend.Entities;

namespace TouchTypingTrainerBackend.Services
{
    /// <summary>
    /// User service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets learning results for user-course.
        /// </summary>
        /// <param name="userId">A user identifier.</param>
        /// <param name="courseId">A Course identifier.</param>
        Task<List<LearningResult>> GetUserLearningResultsAsync(string userId, int courseId);

        /// <summary>
        /// Gets user-related testing results.
        /// </summary>
        /// <param name="userId">A user identifier.</param>
        Task<List<TestingResult>> GetUserTestingResultsAsync(string userId);

        /// <summary>
        /// Gets current user-related exercise.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="courseId">Course idetifier.</param>
        Task<Exercise> GetCurrentExercise(string userId, int courseId);
    }
}
