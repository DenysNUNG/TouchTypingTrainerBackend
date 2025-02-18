using TouchTypingTrainerBackend.Entities;

namespace TouchTypingTrainerBackend.Repositories
{
    /// <summary>
    /// TestingMaterial entity repository.
    /// </summary>
    public interface ITestRepository
    {
        /// <summary>
        /// Gets all testing materials.
        /// </summary>
        /// <param name="layout">Related layout identifier.</param>
        Task<List<TestingMaterial>> GetTestingMaterialsAsync(int layout);
    }
}
