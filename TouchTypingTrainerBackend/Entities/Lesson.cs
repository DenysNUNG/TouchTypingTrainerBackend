using System.Data.Common;

namespace TouchTypingTrainerBackend.Entities
{
    /// <summary>
    /// The Lesson entity.
    /// </summary>
    public class Lesson
    {
        /// <summary>
        /// Lesson identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Lesson title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Lesson description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Related to lesson course identifier.
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Related to lesson course.
        /// </summary>
        public Course Course { get; set; }

        /// <summary>
        /// Lesson exercises.
        /// </summary>
        public List<Exercise> Exercises { get; set; }

        /// <summary>
        /// Maps a lesson.
        /// </summary>
        /// <param name="dr">A reader.</param>
        /// <returns>Mapped lesson.</returns>
        public static Lesson Map(DbDataReader dr)
        {
            return new Lesson
            {
                Id = dr.GetInt32(dr.GetOrdinal("Lesson_UID")),
                Title = dr.GetString(dr.GetOrdinal("Title")),
                Description = dr.GetString(dr.GetOrdinal("Description")),
                CourseId = dr.GetInt32(dr.GetOrdinal("CourseFID"))
            };
        }
    }
}
