using Microsoft.Data.SqlClient;
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
        public async Task<Exercise> GetCurrentExercise(string userId, int courseId)
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
    }
}
