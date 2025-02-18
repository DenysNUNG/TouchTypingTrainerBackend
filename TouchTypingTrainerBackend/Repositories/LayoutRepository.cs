using Microsoft.Data.SqlClient;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Helpers;

namespace TouchTypingTrainerBackend.Repositories
{
    public class LayoutRepository : ILayoutRepository
    {
        readonly private ISprocHelper _sh;

        public LayoutRepository(ISprocHelper sprocHelper)
        {
            _sh = sprocHelper;
        }

        public async Task<List<LayoutKey>> GetCourseLayoutKeysAsync(int courseId)
        {
            var sprocName = "dbo.SelectCourseLayout";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@CourseId", courseId)
            };

            using var dr = await _sh.ExecuteReaderAsync(sprocName, parameters);

            if (!dr.HasRows)
            {
                return default(List<LayoutKey>);
            }

            var courseLayouts = new List<LayoutKey>();

            while (dr.Read())
            {
                var courseLayout = LayoutKey.Map(dr);
                courseLayouts.Add(courseLayout);
            }

            return courseLayouts;
        }

        public async Task<List<Layout>> GetAllLayoutsAsync()
        {
            var sprocName = "dbo.SelectAllLayouts";

            using var dr = await _sh.ExecuteReaderAsync(sprocName, null);

            if (!dr.HasRows)
            {
                return default(List<Layout>);
            }

            var layouts = new List<Layout>();

            while (dr.Read())
            {
                var layout = Layout.Map(dr);
                layouts.Add(layout);
            }

            return layouts;
        }
    }
}
