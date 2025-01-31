using Microsoft.AspNetCore.Identity;
using System.Data.Common;

namespace TouchTypingTrainerBackend.Entities
{
    /// <summary>
    /// User result entity.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// User result identifier.
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
        /// Result type.
        /// 0-Exercise, 1-TestingMaterial
        /// </summary>
        public bool ResultType { get; set; }

        /// <summary>
        /// Related to result user identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Related to result user.
        /// </summary>
        public IdentityUser User { get; set; }

        /// <summary>
        /// Related to result testing material identifier.
        /// </summary>
        public int TestingMaterialId { get; set; }

        /// <summary>
        /// Related to result testing material.
        /// </summary>
        public TestingMaterial TestingMaterial { get; set; }

        /// <summary>
        /// Related to result exercise identifier.
        /// </summary>
        public int ExerciseId { get; set; }

        /// <summary>
        /// Related to result exercise.
        /// </summary>
        public Exercise Exercise { get; set; }

        /// <summary>
        /// Maps an user result.
        /// </summary>
        /// <param name="dr">A reader.</param>
        /// <returns>Mapped UserResult.</returns>
        public static Result Map(DbDataReader dr)
        {
            return new Result
            {
                Id = dr.GetInt32(dr.GetOrdinal("UserResult_UID")),
                Accuracy = dr.GetFloat(dr.GetOrdinal("Accuracy")),
                Speed = dr.GetInt32(dr.GetOrdinal("Speed")),
                ResultType = dr.GetBoolean(dr.GetOrdinal("ResultType")),
                UserId = dr.GetString(dr.GetOrdinal("UserFID")),
                TestingMaterialId = dr.GetInt32(dr.GetOrdinal("TestingMaterialFID")),
                ExerciseId = dr.GetInt32(dr.GetOrdinal("ExerciseFID"))
            };
        }
    }
}
