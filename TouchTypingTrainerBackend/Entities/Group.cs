using System.Data.Common;

namespace TouchTypingTrainerBackend.Entities
{
    /// <summary>
    /// The Group entity.
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Group identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Group name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Maps group.
        /// </summary>
        /// <param name="dr">A reader.</param>
        /// <returns>Mapped group.</returns>
        public static Group Map(DbDataReader dr)
        {
            return new Group
            {
                Id = dr.GetInt32(dr.GetOrdinal("Group_UID")),
                Name = dr.GetString(dr.GetOrdinal("Name"))
            };
        }
    }
}
