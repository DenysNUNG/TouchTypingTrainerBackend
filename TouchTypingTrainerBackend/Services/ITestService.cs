using TouchTypingTrainerBackend.Entities;

namespace TouchTypingTrainerBackend.Services
{
    /// <summary>
    /// Test service.
    /// </summary>
    public interface ITestService
    {
        /// <summary>
        /// Gets random testing material set.
        /// </summary>
        Task<TestingMaterial> GetRandomTestingMaterial();

        /// <summary>
        /// Gets user-related testing results.
        /// </summary>
        /// <param name="userId">A user identifier.</param>
        Task<List<TestingResult>> GetUserTestingResultsAsync(string userId);

        /// <summary>
        /// Adds new user-related testing result.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="testId">Testing material identifier.</param>
        /// <param name="result">User-related testing result.</param>
        Task AddUserTestingResultAsync(string userId, int testId, TestingResult result);
    }
}
