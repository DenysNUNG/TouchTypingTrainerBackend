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
        /// <returns></returns>
        Task<TestingMaterial> GetRandomTestingMaterial();
    }
}
