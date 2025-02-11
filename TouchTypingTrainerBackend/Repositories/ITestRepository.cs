using TouchTypingTrainerBackend.Entities;

namespace TouchTypingTrainerBackend.Repositories
{
    /// <summary>
    /// Repository for TestingMaterial entity.
    /// </summary>
    public interface ITestRepository
    {
        /// <summary>
        /// Gets all testing materials.
        /// </summary>
        Task<List<TestingMaterial>> GetTestingMaterialsAsync();
    }
}
