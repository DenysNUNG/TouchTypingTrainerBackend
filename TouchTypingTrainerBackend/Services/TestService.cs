using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Repositories;

namespace TouchTypingTrainerBackend.Services
{
    /// <summary>
    /// Test service.
    /// </summary>
    public class TestService : ITestService
    {
        /// <summary>
        /// Test repository.
        /// </summary>
        readonly ITestRepository _repo;

        /// <summary>
        /// DI constructor.
        /// </summary>
        public TestService(ITestRepository testRepository)
        {
            _repo = testRepository;
        }

        /// <inheritdoc />
        public async Task<TestingMaterial> GetRandomTestingMaterial()
        {
            var materials = await _repo.GetTestingMaterialsAsync();
            var random = new Random();
            var randomIndex = random.Next(materials.Count);

            var randomMaterial = materials[randomIndex];

            return randomMaterial;
        }
    }
}
