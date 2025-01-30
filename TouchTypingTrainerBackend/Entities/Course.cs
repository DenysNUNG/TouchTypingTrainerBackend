using System.Data.Common;

namespace TouchTypingTrainerBackend.Entities
{
    /// <summary>
    /// The Course entity.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Course identifier.
        /// </summary>
        public int Course_UID { get; set; }

        /// <summary>
        /// Course title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Course description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Course lessons.
        /// </summary>
        public List<Lesson> Lessons { get; set; }

        /// <summary>
        /// Maps a Course.
        /// </summary>
        /// <param name="dr">A reader.</param>
        /// <returns>Mapped course.</returns>
        public static Course Map(DbDataReader dr)
        {
            return new Course
            {
                Course_UID = dr.GetInt32(dr.GetOrdinal("Course_UID")),
                Title = dr.GetString(dr.GetOrdinal("Title")),
                Description = dr.GetString(dr.GetOrdinal("Description"))
            };
        }
    }
}
