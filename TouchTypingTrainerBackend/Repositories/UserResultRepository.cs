using Microsoft.Data.SqlClient;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Helpers;

namespace TouchTypingTrainerBackend.Repositories
{
    /// <summary>
    /// UserResult entity repository.
    /// </summary>
    public class UserResultRepository : IUserResultRepository
    {
        /// <summary>
        /// Stored procedure helper.
        /// </summary>
        readonly private ISprocHelper _sh;

        /// <summary>
        /// DI constructor.
        /// </summary>
        public UserResultRepository(ISprocHelper sprocHelper)
        {
            _sh = sprocHelper;
        }

        /// <inheritdoc />
        public async Task<List<LearningResult>> GetUserLearningResultsAsync(string userId, int courseId)
        {
            var sprocName = "dbo.SelectUserLearningResults";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@CourseId", courseId)
            };

            using var dr = await _sh.ExecuteReaderAsync(sprocName, parameters);

            var results = new List<LearningResult>();

            while (await dr.ReadAsync())
            {
                var result = LearningResult.Map(dr);
                results.Add(result);
            }
            return results;
        }

        /// <inheritdoc />
        public async Task<List<TestingResult>> GetUserTestingResultsAsync(string userId)
        {
            var sprocName = "dbo.SelectUserTestingResults";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId)
            };

            using var dr = await _sh.ExecuteReaderAsync(sprocName, parameters);

            var results = new List<TestingResult>();

            while (await dr.ReadAsync())
            {
                var result = TestingResult.Map(dr);
                results.Add(result);
            }
            return results;
        }

        /// <inheritdoc />
        public async Task AddUserLearningResultAsync(string userId, LearningResult result)
        {
            var sprocName = "dbo.InsertNewUserResult";
            var resultType = 0; // exercise

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Accuracy", result.Accuracy),
                new SqlParameter("@Speed", result.Speed),
                new SqlParameter("@ResultType", resultType),
                new SqlParameter("@UserId", userId),
                new SqlParameter("@ExerciseId", result.ExerciseId)
            };

            await _sh.ExecuteNonQueryAsync(sprocName, parameters);
        }

        /// <inheritdoc />
        public async Task AddUserTestingResultAsync(string userId, int testId, TestingResult result)
        {
            var sprocName = "dbo.InsertNewUserResult";
            var resultType = 1; // testing material

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Accuracy", result.Accuracy),
                new SqlParameter("@Speed", result.Speed),
                new SqlParameter("@ResultType", resultType),
                new SqlParameter("@UserId", userId),
                new SqlParameter("@TestingMaterialId", testId)
            };

            await _sh.ExecuteNonQueryAsync(sprocName, parameters);
        }
    }
}
