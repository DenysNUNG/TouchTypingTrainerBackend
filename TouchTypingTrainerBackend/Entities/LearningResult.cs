using System.Data.Common;
using TouchTypingTrainerBackend.Models;

namespace TouchTypingTrainerBackend.Entities
{
    /// <summary>
    /// The user's learning result.
    /// </summary>
    public class LearningResult : IUserResult
    {
        /// <summary>
        /// Learning result identifier.
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
        /// Related exercise identifier.
        /// </summary>
        public int ExerciseId { get; set; }

        /// <summary>
        /// Related exercise.
        /// </summary>
        public Exercise Exercise { get; set; }

        /// <summary>
        /// Date the user took an exercise.
        /// </summary>
        public DateOnly CreatedAt { get; set; }

        /// <summary>
        /// Maps learning result.
        /// </summary>
        /// <param name="dr">A reader.</param>
        public static LearningResult Map(DbDataReader dr)
        {
            return new LearningResult
            {
                Id = dr.GetInt32(dr.GetOrdinal("UserResult_UID")),
                Accuracy = dr.GetFloat(dr.GetOrdinal("Accuracy")),
                Speed = dr.GetInt32(dr.GetOrdinal("Speed")),
                ExerciseId = dr.GetInt32(dr.GetOrdinal("ExerciseFID")),
                CreatedAt = dr.GetFieldValue<DateOnly>(dr.GetOrdinal("CreatedAt"))
            };
        }
    }
}
