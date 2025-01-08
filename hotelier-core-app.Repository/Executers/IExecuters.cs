using Microsoft.Data.SqlClient;

namespace hotelier_core_app.Domain.Executers
{
    public interface IExecuters : IAutoDependencyRepository
    {
        void ExecuteCommand(string connStr, Action<SqlConnection, SqlTransaction> task);

        T ExecuteCommand<T>(string connStr, Func<SqlConnection, SqlTransaction, T> task);

        Task<T> ExecuteCommandAsync<T>(string connStr, Func<SqlConnection, SqlTransaction, Task<T>> task);

        dynamic ExecuteCommand<T>(string connStr, string query, object param);

        Task<dynamic> ExecuteCommandAsync<T>(string connStr, string query, object param);

        IEnumerable<T> ExecuteReader<T>(string connStr, Func<SqlConnection, SqlTransaction, IEnumerable<T>> task);

        IEnumerable<T> ExecuteReader<T>(string connStr, string query, object param);

        Task<IEnumerable<T>> ExecuteReaderAsync<T>(string connStr, string query, object param);

        Task<IEnumerable<T>> ExecuteReaderWithIncludeAsync<T, T1>(string connStr, string query, Func<T, T1, T> map, object param);

        Task<T> ExecuteSingleReaderAsync<T>(string connStr, string query, object param);

        Task<IEnumerable<T>> ExecuteReaderAsync<T>(string connStr, Func<SqlConnection, SqlTransaction, Task<IEnumerable<T>>> task);

        Task<dynamic> ExecuteCommandAsync<T>(string query, object param, SqlTransaction sqlTransaction);

        dynamic ExecuteCommand<T>(string query, object param, SqlTransaction sqlTransaction);
    }
}
