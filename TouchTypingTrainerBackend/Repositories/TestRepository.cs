using Microsoft.Data.SqlClient;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Helpers;

namespace TouchTypingTrainerBackend.Repositories
{
    /// <summary>
    /// Repository for TestingMaterial entity.
    /// </summary>
    public class TestRepository : ITestRepository
    {
        /// <summary>
        /// Stored procedure helper.
        /// </summary>
        readonly private ISprocHelper _sh;

        /// <summary>
        /// DI constructor.
        /// </summary>
        public TestRepository(ISprocHelper sprocHelper)
        {
            _sh = sprocHelper;
        }

        /// <inheritdoc />
        public async Task<List<TestingMaterial>> GetTestingMaterialsAsync()
        {
            var sprocName = "dbo.SelectAllTestingMaterials";
            var parameters = Array.Empty<SqlParameter>();

            using var dr = await _sh.ExecuteReaderAsync(sprocName, parameters);

            if (!dr.HasRows)
            {
                return default(List<TestingMaterial>);
            }

            var materials = new List<TestingMaterial>();

            while (await dr.ReadAsync())
            {
                var tm = TestingMaterial.Map(dr);
                materials.Add(tm);
            }

            return materials;
        }
    }
}
