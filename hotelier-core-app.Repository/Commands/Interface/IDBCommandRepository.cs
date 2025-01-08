using Microsoft.Data.SqlClient;

namespace hotelier_core_app.Domain.Commands.Interface
{
    public interface IDBCommandRepository<TEntity> : IBaseCommandRepository<TEntity> where TEntity : class
    {
        void AddWithTransaction(TEntity entity, SqlTransaction transaction);

        Task AddWithTransactionAsync(TEntity entity, SqlTransaction transaction);

        void AddRangeWithTransaction(List<TEntity> entity, SqlTransaction transaction);

        Task AddRangeWithTransactionAsync(List<TEntity> entity, SqlTransaction transaction);

        void UpdateWithTransaction(TEntity entity, SqlTransaction transaction);

        Task UpdateWithTransactionAsync(TEntity entity, SqlTransaction transaction);

        void UpdateRangeWithTransaction(IEnumerable<TEntity> entities, SqlTransaction transaction);

        Task UpdateRangeWithTransactionAsync(IEnumerable<TEntity> entities, SqlTransaction transaction);

        void DeleteWithTransaction(object id, SqlTransaction transaction);

        Task DeleteWithTransactionAsync(object id, SqlTransaction transaction);

        SqlTransaction BeginTransaction();

        Task<SqlTransaction> BeginTransactionAsync();

        SqlTransaction BeginTransaction(string connectionString);

        Task<SqlTransaction> BeginTransactionAsync(string connectionString);

        void CommitTransaction(SqlTransaction sqlTransaction);

        Task CommitTransactionAsync(SqlTransaction sqlTransaction);

        void RollBackTransaction(SqlTransaction sqlTransaction);

        Task RollBackTransactionAsync(SqlTransaction sqlTransaction);
    }
}
