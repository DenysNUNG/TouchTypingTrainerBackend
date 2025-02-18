using System.Data.Common;

namespace TouchTypingTrainerBackend.Entities
{
    public class LayoutKey
    {
        public int Index { get; set; }
        public char Value { get; set; }
        public char ShiftValue { get; set; }

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
