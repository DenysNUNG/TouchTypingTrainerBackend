using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace TouchTypingTrainerBackend.Helpers
{
    public class SprocHelper : IDisposable
    {
        private readonly SqlConnection _cnn;
        private SqlCommand _cd;
        private SqlDataReader _rd;

        private bool _disposed = false;

        public SprocHelper(string connectionString)
        {
            _cnn = new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _rd?.Dispose();
                    _cd?.Dispose();
                    _cnn?.Dispose();
                }

                _disposed = true;
            }
        }

        ~SprocHelper()
        {
            Dispose(false);
        }

        public async Task<SqlDataReader> ExecuteReaderAsync(string sprocName,
            SqlParameter[] parameters)
        {
            await _cnn.OpenAsync();

            _cd = _cnn.CreateCommand();
            _cd.CommandType = CommandType.StoredProcedure;
            _cd.CommandText = sprocName;

            if (!parameters.IsNullOrEmpty())
            {
                _cd.Parameters.AddRange(parameters);
            }

            return await _cd.ExecuteReaderAsync();
        }
    }
}
