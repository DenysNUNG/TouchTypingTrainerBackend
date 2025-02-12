using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Repositories;

namespace TouchTypingTrainerBackend.Services
{
    /// <summary>
    /// Typing tutorial service.
    /// </summary>
    public class TutorialService : ITutorialService
    {
        /// <summary>
        /// A course repository.
        /// </summary>
        readonly private ICourseRepository _courseRepo;

        /// <summary>
        /// User result repository.
        /// </summary>
        readonly private IUserResultRepository _resultRepo;

        /// <summary>
        /// User progress repository.
        /// </summary>
        readonly private IProgressRepository _progressRepo;

        /// <summary>
        /// DI constructor.
        /// </summary>
        public TutorialService(ICourseRepository courseRepository,
            IUserResultRepository userResultRepository,
            IProgressRepository progressRepository)
        {
            _courseRepo = courseRepository;
            _resultRepo = userResultRepository;
            _progressRepo = progressRepository;
        }

        /// <inheritdoc />
        public async Task<Course> GetCourseByIdAsync(int id, bool includeLessonsWithExercises)
        {
            return await _courseRepo.GetCourseByIdAsync(id, includeLessonsWithExercises);
        }

        /// <inheritdoc />
        public async Task<List<Course>> GetCoursesAsync()
        {
            return await _courseRepo.GetCoursesAsync();
        }

        /// <inheritdoc />
        public async Task<List<LearningResult>> GetUserLearningResultsAsync(string userId,
            int courseId)
        {
            return await _resultRepo.GetUserLearningResultsAsync(userId, courseId);
        }

        /// <inheritdoc />
        public async Task<Exercise> GetCurrentExercise(string userId, int courseId)
        {
            return await _progressRepo.GetCurrentExercise(userId, courseId);
        }
    }
}
