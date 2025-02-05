using Microsoft.Data.SqlClient;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Helpers;

namespace TouchTypingTrainerBackend.Repositories
{
    /// <summary>
    /// Course entity repository.
    /// </summary>
    public class CourseRepository : ICourseRepository
    {
        /// <summary>
        /// A stored procedures helper.
        /// </summary>
        readonly private ISprocHelper _sh;

        /// <summary>
        /// DI constructor.
        /// </summary>
        /// <param name="sprocHelper">A stored procedures helper.</param>
        public CourseRepository(ISprocHelper sprocHelper)
        {
            _sh = sprocHelper;
        }

        /// <inheritdoc />
        public async Task<Course> GetCourseByIdAsync(int id,
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

        /// <inheritdoc />
        public async Task<List<Course>> GetCoursesAsync()
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
