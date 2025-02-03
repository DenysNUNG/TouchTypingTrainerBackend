using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Repositories;

namespace TouchTypingTrainerBackend.Services
{
    public class TypingService : ITypingService
    {
        readonly private ICourseRepository _repo;

        public TypingService(ICourseRepository courseRepo)
        {
            _repo = courseRepo;
        }

        public async Task<Course> GetCourseById(int id, bool includeLessonsWithExercises)
        {
            return await _repo.GetCourseById(id, includeLessonsWithExercises);
        }

        public async Task<List<Course>> GetCourses()
        {
            return await _repo.GetCourses();
        }
    }
}
