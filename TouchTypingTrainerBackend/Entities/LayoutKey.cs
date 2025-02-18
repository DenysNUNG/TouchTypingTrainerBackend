using System.Data.Common;

namespace TouchTypingTrainerBackend.Entities
{
    /// <summary>
    /// Layout key entity.
    /// </summary>
    public class LayoutKey
    {
        /// <summary>
        /// The key index.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// The key value.
        /// </summary>
        public char Value { get; set; }

        /// <summary>
        /// The key shift value.
        /// </summary>
        public char ShiftValue { get; set; }

        /// <summary>
        /// Maps a layout key.
        /// </summary>
        /// <param name="dr">A reader.</param>
        public static LayoutKey Map(DbDataReader dr)
        {
            return new LayoutKey
            {
                Index = dr.GetInt32(dr.GetOrdinal("KeyFID")),
                Value = dr.GetString(dr.GetOrdinal("Value")).Trim()[0],
                ShiftValue = dr.GetString(dr.GetOrdinal("ShiftValue")).Trim()[0]
            };
        }
    }
}
