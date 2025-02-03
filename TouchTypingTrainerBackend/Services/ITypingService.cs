using TouchTypingTrainerBackend.Entities;

namespace TouchTypingTrainerBackend.Services
{
    public interface ITypingService
    {
        Task<List<Course>> GetCourses();
        Task<Course> GetCourseById(int id, bool includeLessonsWithExercises);
    }
}
