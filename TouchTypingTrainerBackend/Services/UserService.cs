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

        readonly private IProgressRepository _progressRepo;

        /// <summary>
        /// DI constructor.
        /// </summary>
        public UserService(IUserResultRepository userResultRepository,
            IProgressRepository progressRepository)
        {
            _resultRepo = userResultRepository;
            _progressRepo = progressRepository;
        }

        /// <inheritdoc />
        public async Task<List<LearningResult>> GetUserLearningResultsAsync(string userId,
            int courseId)
        {
            return await _resultRepo.GetUserLearningResultsAsync(userId, courseId);
        }

        /// <inheritdoc />
        public async Task<List<TestingResult>> GetUserTestingResultsAsync(string userId)
        {
            return await _resultRepo.GetUserTestingResultsAsync(userId);
        }

        /// <inheritdoc />
        public async Task<Exercise> GetCurrentExercise(string userId, int courseId)
        {
            return await _progressRepo.GetCurrentExercise(userId, courseId);
        }
    }
}
