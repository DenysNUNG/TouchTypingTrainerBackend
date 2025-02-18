using System.Data.Common;

namespace TouchTypingTrainerBackend.Entities
{
    /// <summary>
    /// The testing material entity.
    /// </summary>
    public class TestingMaterial
    {
        /// <summary>
        /// Testing material identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Testing material text resourse.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Related testing material layout.
        /// </summary>
        public int LayoutId { get; set; }

        /// <summary>
        /// Maps testing material.
        /// </summary>
        /// <param name="dr">A reader.</param>
        /// <returns>Mapped testing material.</returns>
        public static TestingMaterial Map(DbDataReader dr)
        {
            return new TestingMaterial
            {
                Id = dr.GetInt32(dr.GetOrdinal("TestingMaterial_UID")),
                Text = dr.GetString(dr.GetOrdinal("Text")),
                LayoutId = dr.GetInt32(dr.GetOrdinal("LayoutFID"))
            };
        }
    }
}
