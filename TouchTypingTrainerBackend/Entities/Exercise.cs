using System.Data.Common;

namespace TouchTypingTrainerBackend.Entities
{
    /// <summary>
    /// The exercise entity.
    /// </summary>
    public class Exercise
    {
        /// <summary>
        /// Exercise identifier.
        /// </summary>
        public int Exercise_UID { get; set; }

        /// <summary>
        /// Exercise title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Exercise study set.
        /// </summary>
        public string StudySet { get; set; }

        /// <summary>
        /// Related to exercise lesson identifier.
        /// </summary>
        public int LessonFID { get; set; }

        /// <summary>
        /// Related to exercise lesson.
        /// </summary>
        public Lesson Lesson { get; set; }

        /// <summary>
        /// Maps an exercise.
        /// </summary>
        /// <param name="dr">A reader.</param>
        /// <returns>Mapped exercise.</returns>
        public static Exercise Map(DbDataReader dr)
        {
            return new Exercise
            {
                Exercise_UID = dr.GetInt32(dr.GetOrdinal("Exercise_UID")),
                Title = dr.GetString(dr.GetOrdinal("Title")),
                StudySet = dr.GetString(dr.GetOrdinal("StudySet")),
                LessonFID = dr.GetInt32(dr.GetOrdinal("LessonFID"))
            };
        }
    }
}
