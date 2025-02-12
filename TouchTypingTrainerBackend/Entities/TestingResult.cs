using System.Data.Common;
using TouchTypingTrainerBackend.Models;

namespace TouchTypingTrainerBackend.Entities
{
    /// <summary>
    /// The user's testing result.
    /// </summary>
    public class TestingResult : IUserResult
    {
        /// <summary>
        /// Testing result identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Result accuracy.
        /// </summary>
        public float Accuracy { get; set; }

        /// <summary>
        /// Result speed.
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Maps testing result.
        /// </summary>
        /// <param name="dr">A reader.</param>
        public static TestingResult Map(DbDataReader dr)
        {
            return new TestingResult
            {
                Id = dr.GetInt32(dr.GetOrdinal("UserResult_UID")),
                Accuracy = dr.GetFloat(dr.GetOrdinal("Accuracy")),
                Speed = dr.GetInt32(dr.GetOrdinal("Speed"))
            };
        }
    }
}
