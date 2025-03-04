using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Helpers;

namespace TouchTypingTrainerBackend.Repositories
{
    /// <summary>
    /// UserCourseProgress entity repository.
    /// </summary>
    public class ProgressRepository : IProgressRepository
    {
        /// <summary>
        /// Stored procedure helper.
        /// </summary>
        readonly private ISprocHelper _sh;

        /// <summary>
        /// DI constructor.
        /// </summary>
        public ProgressRepository(ISprocHelper sprocHelper)
        {
            _sh = sprocHelper;
        }

        /// <inheritdoc />
        public async Task<Exercise> GetCurrentExerciseAsync(string userId, int courseId)
        {
            var sprocName = "dbo.SelectCurrentExercise";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@CourseId", courseId)
            };

            using var dr = await _sh.ExecuteReaderAsync(sprocName, parameters);

            if (!dr.HasRows)
            {
                return default(Exercise);
            }

            await dr.ReadAsync();
            var exercise = Exercise.Map(dr);

            return exercise;
        }

        /// <inheritdoc />
        public async Task UpsertUserCourseProgressAsync(string userId, int courseId)
        {
            var sprocName = "dbo.UpsertUserCourseProgress";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@CourseId", courseId)
            };

            await _sh.ExecuteNonQueryAsync(sprocName, parameters);
        }

        /// <inheritdoc />
        public async Task<List<Course>> GetUserCoursesAsync(string userId)
        {
            var sprocName = "dbo.SelectUserCourses";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId)
            };

            using var dr = await _sh.ExecuteReaderAsync(sprocName, parameters);

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

        /// <inheritdoc />
        public async Task<Exercise> ChooseExerciseAsync(string userId, int courseId, int exerciseId)
        {
            var sprocName = "dbo.ChooseExercise";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@CourseId", courseId),
                new SqlParameter("@ExerciseId", exerciseId)
            };

            using var dr = await _sh.ExecuteReaderAsync(sprocName, parameters);

            if (!dr.HasRows) 
            {
                return default(Exercise);
            }

            await dr.ReadAsync();
            var exercise = Exercise.Map(dr);

            return exercise;
        }
    }
}
