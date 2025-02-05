using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace TouchTypingTrainerBackend.Helpers
{
    /// <summary>
    /// Helps repositories to deal with stored procedures.
    /// </summary>
    public interface ISprocHelper
    {
        /// <summary>
        /// Executes parametrized or non-parametrized stored procedure.
        /// </summary>
        /// <param name="sprocName">Stored procedure name.</param>
        /// <param name="parameters">Sql-parameters.</param>
        Task<DbDataReader> ExecuteReaderAsync(string sprocName, SqlParameter[] parameters);
    }
}
