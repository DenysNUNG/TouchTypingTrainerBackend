using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Repositories;

namespace TouchTypingTrainerBackend.Services
{
    /// <summary>
    /// User service.
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// User result repository.
        /// </summary>
        readonly private IUserResultRepository _resultRepo;

        /// <summary>
        /// DI constructor.
        /// </summary>
        public UserService(IUserResultRepository userResultRepository)
        {
            _resultRepo = userResultRepository;
        }

        /// <inheritdoc />
        public async Task<List<LearningResult>> GetUserLearningResultsAsync(string userId, int courseId)
        {
            return await _resultRepo.GetUserLearningResultsAsync(userId, courseId);
        }

        /// <inheritdoc />
        public async Task<List<TestingResult>> GetUserTestingResultsAsync(string userId)
        {
            return await _resultRepo.GetUserTestingResultsAsync(userId);
        }
    }
}
