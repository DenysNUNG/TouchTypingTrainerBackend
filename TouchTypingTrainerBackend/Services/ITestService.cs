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
    }
}
