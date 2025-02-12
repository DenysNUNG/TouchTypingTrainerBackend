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
        Task<List<TestingMaterial>> GetTestingMaterialsAsync();
    }
}
