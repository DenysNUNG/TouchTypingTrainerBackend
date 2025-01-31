using Microsoft.AspNetCore.Identity;
using System.Data.Common;

namespace TouchTypingTrainerBackend.Entities
{
    /// <summary>
    /// User course progress entity.
    /// </summary>
    public class Progress
    {
        /// <summary>
        /// User course progress identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Progress flag that indicates whether the course has been completed or not.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Related to progress current exercise identifier.
        /// </summary>
        public int CurrentExerciseId { get; set; }

        /// <summary>
        /// Related to progress current exercise.
        /// </summary>
        public Exercise CurrentExercise { get; set; }

        /// <summary>
        /// Related to progress current lesson identifier.
        /// </summary>
        public int CurrentLessonId { get; set; }

        /// <summary>
        /// Related to progress current lesson.
        /// </summary>
        public Lesson CurrentLesson { get; set; }

        /// <summary>
        /// Related to progress course identifier.
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Related to progress course.
        /// </summary>
        public Course Course { get; set; }

        /// <summary>
        /// Related to progress user identifier.
        /// </summary>
        public string UserFID { get; set; }

        /// <summary>
        /// Related to progress user.
        /// </summary>
        public IdentityUser User { get; set; }

        /// <summary>
        /// Maps an user course progress.
        /// </summary>
        /// <param name="dr">A reader.</param>
        /// <returns>Mapped UserCourseProgress.</returns>
        public static Progress Map(DbDataReader dr)
        {
            return new Progress
            {
                Id = dr.GetInt32(dr.GetOrdinal("UserCourseProgress_UID")),
                IsCompleted = dr.GetBoolean(dr.GetOrdinal("IsCompleted")),
                CurrentExerciseId = dr.GetInt32(dr.GetOrdinal("CurrentExerciseFID")),
                CurrentLessonId = dr.GetInt32(dr.GetOrdinal("CurrentLessonFID")),
                CourseId = dr.GetInt32(dr.GetOrdinal("CourseFID")),
                UserFID = dr.GetString(dr.GetOrdinal("UserFID"))
            };
        }
    }
}
