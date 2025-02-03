using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace TouchTypingTrainerBackend.Helpers
{
    /// <summary>
    /// Helps repositories to deal with stored procedures.
    /// </summary>
    public class SprocHelper : IDisposable
    {
        /// <summary>
        /// Sql connection.
        /// </summary>
        private readonly SqlConnection _cnn;

        /// <summary>
        /// Sql command.
        /// </summary>
        private SqlCommand _cd;

        /// <summary>
        /// Sql data reader.
        /// </summary>
        private SqlDataReader _rd;

        /// <summary>
        /// Dispose flag.
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// DI constructor.
        /// </summary>
        /// <param name="connectionString">A connection string.</param>
        public SprocHelper(string connectionString)
        {
            _cnn = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Disposes the resources used by the SprocHelper.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the resources used by the SprocHelper.
        /// </summary>
        /// <param name="disposing">Indicates whether the method is being called
        /// from the Dispose method.</param>
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

        /// <summary>
        /// Finalizer for the SprocHelper to clean up
        /// resources if Dispose is not called.
        /// </summary>
        ~SprocHelper()
        {
            Dispose(false);
        }

        /// <summary>
        /// Executes parametrized or non-parametrized stored procedure.
        /// </summary>
        /// <param name="sprocName">Stored procedure name.</param>
        /// <param name="parameters">Sql-parameters.</param>
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
