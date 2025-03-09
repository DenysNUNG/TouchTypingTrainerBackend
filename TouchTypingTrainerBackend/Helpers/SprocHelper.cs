using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.Common;

namespace TouchTypingTrainerBackend.Helpers
{
    /// <summary>
    /// Helps repositories to deal with stored procedures.
    /// </summary>
    public class SprocHelper : IAsyncDisposable, ISprocHelper
    {
        /// <summary>
        /// Sql connection.
        /// </summary>
        private readonly SqlConnection _cnn;

        /// <summary>
        /// Sql command.
        /// </summary>
        private SqlCommand? _cd;

        /// <summary>
        /// Sql data reader.
        /// </summary>
        private SqlDataReader? _rd;

        /// <summary>
        /// DI constructor.
        /// </summary>
        /// <param name="connectionString">A connection string.</param>
        public SprocHelper(string connectionString)
        {
            _cnn = new SqlConnection(connectionString);
        }

        /// <inheritdoc />
        public async Task<DbDataReader> ExecuteReaderAsync(string sprocName,
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

        /// <inheritdoc />
        public async Task ExecuteNonQueryAsync(string sprocName,
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

            await _cd.ExecuteNonQueryAsync();
        }

        /// <inheritdoc />
        public async ValueTask DisposeAsync()
        {
            if (_rd != null)
            {
                await _rd.DisposeAsync();
            }

            if (_cd != null)
            {
                await _cd.DisposeAsync();
            }

            await _cnn.DisposeAsync();
        }
    }
}
