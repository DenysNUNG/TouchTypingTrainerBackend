using TouchTypingTrainerBackend.Entities;

namespace TouchTypingTrainerBackend.Repositories
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetCourses();
        Task<Course> GetCourseById(int id, bool includeLessonsWithExercises);
    }
}
