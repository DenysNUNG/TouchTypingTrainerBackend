using System.Data.Common;

namespace TouchTypingTrainerBackend.Entities
{
    public class Layout
    {
        public int LayoutId { get; set; }
        public string Name { get; set; }

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
