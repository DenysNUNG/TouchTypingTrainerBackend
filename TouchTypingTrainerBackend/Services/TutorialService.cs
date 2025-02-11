using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Repositories;

namespace TouchTypingTrainerBackend.Services
{
    public class TutorialService : ITutorialService
    {
        /// <summary>
        /// A course repository.
        /// </summary>
        readonly private ICourseRepository _repo;

        /// <summary>
        /// DI constructor.
        /// </summary>
        /// <param name="courseRepo">A course repository.</param>
        public TutorialService(ICourseRepository courseRepo)
        {
            _repo = courseRepo;
        }

        /// <inheritdoc />
        public async Task<Course> GetCourseByIdAsync(int id, bool includeLessonsWithExercises)
        {
            return await _repo.GetCourseByIdAsync(id, includeLessonsWithExercises);
        }

        /// <inheritdoc />
        public async Task<List<Course>> GetCoursesAsync()
        {
            return await _repo.GetCoursesAsync();
        }
    }
}
