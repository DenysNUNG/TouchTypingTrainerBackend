using TouchTypingTrainerBackend.Entities;

namespace TouchTypingTrainerBackend.Repositories
{
    /// <summary>
    /// UserResult entity repository.
    /// </summary>
    public interface IUserResultRepository
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
        /// Adds new user-related learning result.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="result">User-related learning result.</param>
        Task AddUserLearningResultAsync(string userId, LearningResult result);

        /// <summary>
        /// Adds new user-related testing result.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="testId">Testing material identifier.</param>
        /// <param name="result">User-related testing result.</param>
        Task AddUserTestingResultAsync(string userId, int testId, TestingResult result);
    }
}
