using Microsoft.Data.SqlClient;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Helpers;

namespace TouchTypingTrainerBackend.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        readonly private SprocHelper _sh;

        public CourseRepository(SprocHelper sprocHelper)
        {
            _sh = sprocHelper;
        }

        public async Task<Course> GetCourseById(int id,
            bool IncludeLessonsWithExercises)
        {
            var sprocName = "dbo.SelectCourseById";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CourseId", id),
                new SqlParameter("@IncludeLessonsWithExercises",
                    IncludeLessonsWithExercises)
            };

            using var dr = await _sh.ExecuteReaderAsync(sprocName, parameters);

            if (!dr.HasRows)
            {
                return default(Course);
            }

            await dr.ReadAsync();
            Course course = Course.Map(dr);

            if (await dr.NextResultAsync())
            {
                List<Lesson> lessons = new List<Lesson>();

                while (await dr.ReadAsync())
                {
                    var lesson = Lesson.Map(dr);
                    lessons.Add(lesson);
                }

                if (await dr.NextResultAsync())
                {
                    List<Exercise> exercises = new List<Exercise>();

                    while (await dr.ReadAsync())
                    {
                        var exercise = Exercise.Map(dr);
                        exercises.Add(exercise);
                    }

                    foreach (var lesson in lessons)
                    {
                        lesson.Exercises = exercises
                            .Where(e => e.LessonId == lesson.Id)
                            .ToList();
                    }
                }
                course.Lessons = lessons;
            }

            return course;
        }

        public async Task<List<Course>> GetCourses()
        {
            var sprocName = "dbo.SelectAllCourses";
            using var dr = await _sh.ExecuteReaderAsync(sprocName, null);

            if (!dr.HasRows)
            {
                return default(List<Course>);
            }

            var courses = new List<Course>();

            while (await dr.ReadAsync())
            {
                var course = Course.Map(dr);
                courses.Add(course);
            }

            return courses;
        }
    }
}
