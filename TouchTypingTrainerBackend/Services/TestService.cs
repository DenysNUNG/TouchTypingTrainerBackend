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
        readonly ITestRepository _testRepo;

        /// <summary>
        /// User result repository.
        /// </summary>
        readonly private IUserResultRepository _resultRepo;

        /// <summary>
        /// DI constructor.
        /// </summary>
        public TestService(ITestRepository testRepository,
            IUserResultRepository userResultRepository)
        {
            _testRepo = testRepository;
            _resultRepo = userResultRepository;
        }

        /// <inheritdoc />
        public async Task<TestingMaterial> GetRandomTestingMaterial()
        {
            var materials = await _testRepo.GetTestingMaterialsAsync();
            var random = new Random();
            var randomIndex = random.Next(materials.Count);

            var randomMaterial = materials[randomIndex];

            return randomMaterial;
        }

        /// <inheritdoc />
        public async Task<List<TestingResult>> GetUserTestingResultsAsync(string userId)
        {
            return await _resultRepo.GetUserTestingResultsAsync(userId);
        }

        /// <inheritdoc />
        public async Task AddUserTestingResultAsync(string userId,
            int testId,
            TestingResult result)
        {
            await _resultRepo.AddUserTestingResultAsync(userId, testId, result);
        }
    }
}
