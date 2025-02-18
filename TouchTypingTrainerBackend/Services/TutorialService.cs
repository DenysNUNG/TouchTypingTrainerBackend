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

        readonly private ILayoutRepository _layoutRepo;

        /// <summary>
        /// DI constructor.
        /// </summary>
        public TutorialService(ICourseRepository courseRepository,
            IUserResultRepository userResultRepository,
            IProgressRepository progressRepository,
            ILayoutRepository layoutRepo)
        {
            _courseRepo = courseRepository;
            _resultRepo = userResultRepository;
            _progressRepo = progressRepository;
            _layoutRepo = layoutRepo;
        }

        /// <inheritdoc />
        public async Task<Course> GetCourseByIdAsync(int id,
            bool includeLessonsWithExercises)
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
        public async Task<Exercise> GetCurrentExerciseAsync(string userId, int courseId)
        {
            return await _progressRepo.GetCurrentExerciseAsync(userId, courseId);
        }

        /// <inheritdoc />
        public async Task AddUserLearningResultAsync(string userId, LearningResult result)
        {
            await _resultRepo.AddUserLearningResultAsync(userId, result);
        }

        /// <inheritdoc />
        public async Task UpsertUserCourseProgressAsync(string userId, int courseId)
        {
            await _progressRepo.UpsertUserCourseProgressAsync(userId, courseId);
        }

        /// <inheritdoc />
        public async Task<List<Course>> GetUserCoursesAsync(string userId)
        {
            return await _progressRepo.GetUserCoursesAsync(userId);
        }

        public async Task<List<LayoutKey>> GetCourseLayoutKeysAsync(int courseId)
        {
            return await _layoutRepo.GetCourseLayoutKeysAsync(courseId);
        }

        public async Task<List<Layout>> GetAllLayoutsAsync()
        {
            return await _layoutRepo.GetAllLayoutsAsync();
        }
    }
}
