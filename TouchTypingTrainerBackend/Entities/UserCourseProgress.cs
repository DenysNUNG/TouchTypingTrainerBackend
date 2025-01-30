using Microsoft.AspNetCore.Identity;
using System.Data.Common;

namespace TouchTypingTrainerBackend.Entities
{
    /// <summary>
    /// User course progress entity.
    /// </summary>
    public class UserCourseProgress
    {
        /// <summary>
        /// User course progress identifier.
        /// </summary>
        public int UserCourseProgress_UID { get; set; }

        /// <summary>
        /// Progress flag that indicates whether the course has been completed or not.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Related to progress current exercise identifier.
        /// </summary>
        public int CurrentExerciseFID { get; set; }

        /// <summary>
        /// Related to progress current exercise.
        /// </summary>
        public Exercise CurrentExercise { get; set; }

        /// <summary>
        /// Related to progress current lesson identifier.
        /// </summary>
        public int CurrentLessonFID { get; set; }

        /// <summary>
        /// Related to progress current lesson.
        /// </summary>
        public Lesson CurrentLesson { get; set; }

        /// <summary>
        /// Related to progress course identifier.
        /// </summary>
        public int CourseFID { get; set; }

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
        public static UserCourseProgress Map(DbDataReader dr)
        {
            return new UserCourseProgress
            {
                UserCourseProgress_UID = dr.GetInt32(dr.GetOrdinal("UserCourseProgress_UID")),
                IsCompleted = dr.GetBoolean(dr.GetOrdinal("IsCompleted")),
                CurrentExerciseFID = dr.GetInt32(dr.GetOrdinal("CurrentExerciseFID")),
                CurrentLessonFID = dr.GetInt32(dr.GetOrdinal("CurrentLessonFID")),
                CourseFID = dr.GetInt32(dr.GetOrdinal("CourseFID")),
                UserFID = dr.GetString(dr.GetOrdinal("UserFID"))
            };
        }
    }
}
