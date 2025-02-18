using System.Data.Common;

namespace TouchTypingTrainerBackend.Entities
{
    /// <summary>
    /// Layout entity.
    /// </summary>
    public class Layout
    {
        /// <summary>
        /// Layout identifier.
        /// </summary>
        public int LayoutId { get; set; }

        /// <summary>
        /// Layout name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Maps a layout.
        /// </summary>
        /// <param name="dr">A reader.</param>
        public static Layout Map(DbDataReader dr)
        {
            return new Layout()
            {
                LayoutId = dr.GetInt32(dr.GetOrdinal("Layout_UID")),
                Name = dr.GetString(dr.GetOrdinal("Name"))
            };
        }
    }
}
