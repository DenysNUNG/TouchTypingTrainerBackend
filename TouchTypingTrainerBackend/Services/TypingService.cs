using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Repositories;

namespace TouchTypingTrainerBackend.Services
{
    public class TypingService : ITypingService
    {
        /// <summary>
        /// A course repository.
        /// </summary>
        readonly private ICourseRepository _repo;

        /// <summary>
        /// DI constructor.
        /// </summary>
        /// <param name="courseRepo">A course repository.</param>
        public TypingService(ICourseRepository courseRepo)
        {
            _repo = courseRepo;
        }

        /// <inheritdoc />
        public async Task<Course> GetCourseById(int id, bool includeLessonsWithExercises)
        {
            return await _repo.GetCourseById(id, includeLessonsWithExercises);
        }

        /// <inheritdoc />
        public async Task<List<Course>> GetCourses()
        {
            return await _repo.GetCourses();
        }
    }
}
