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
                Lesson currentLesson = default(Lesson);

                while (await dr.ReadAsync())
                {
                    var lesson = Lesson.Map(dr);

                    if (currentLesson == default(Lesson) || currentLesson.Id != lesson.Id)
                    {
                        currentLesson = lesson;
                        lessons.Add(currentLesson);
                    }

                    var exerciseIdIndex = dr.GetOrdinal("Exercise_UID");

                    if (!dr.IsDBNull(exerciseIdIndex))
                    {
                        var exercise = Exercise.Map(dr);
                        currentLesson.Exercises.Add(exercise);
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
