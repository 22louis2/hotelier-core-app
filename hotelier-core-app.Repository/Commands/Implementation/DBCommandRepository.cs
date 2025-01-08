using Autofac.Features.Indexed;
using hotelier_core_app.Domain.Commands.Interface;
using hotelier_core_app.Domain.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace hotelier_core_app.Domain.Commands.Implementation
{

    public class DBCommandRepository<TEntity> : IDBCommandRepository<TEntity>, IBaseCommandRepository<TEntity> where TEntity : class
    {
        private readonly IDBCommandRepository<TEntity> _commandRepository;

        private readonly IConfiguration _configuration;

        public DBCommandRepository(IIndex<DBProvider, IDBCommandRepository<TEntity>> commandRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            if (!Enum.TryParse<DBProvider>(_configuration.GetValue<string>("AppSettings:OrmType"), ignoreCase: true, out var result))
            {
                result = DBProvider.SQL_Dapper;
            }

            _commandRepository = commandRepository[result];
        }

        public object AddSoft(TEntity entity)
        {
            return _commandRepository.AddSoft(entity);
        }

        public async Task<object> AddSoftAsync(TEntity entity)
        {
            return await _commandRepository.AddSoftAsync(entity);
        }

        public object Add(TEntity entity)
        {
            return _commandRepository.Add(entity);
        }

        public async Task<object> AddAsync(TEntity entity)
        {
            return await _commandRepository.AddAsync(entity);
        }

        public void AddRange(List<TEntity> entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddRangeAsync(List<TEntity> entity)
        {
            await _commandRepository.AddRangeAsync(entity);
        }

        public void AddRangeWithTransaction(List<TEntity> entity, SqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeWithTransactionAsync(List<TEntity> entity, SqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void AddWithTransaction(TEntity entity, SqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public async Task AddWithTransactionAsync(TEntity entity, SqlTransaction transaction)
        {
            await _commandRepository.AddWithTransactionAsync(entity, transaction);
        }

        public SqlTransaction BeginTransaction()
        {
            return _commandRepository.BeginTransaction();
        }

        public SqlTransaction BeginTransaction(string connectionString)
        {
            throw new NotImplementedException();
        }

        public Task<SqlTransaction> BeginTransactionAsync()
        {
            return _commandRepository.BeginTransactionAsync();
        }

        public Task<SqlTransaction> BeginTransactionAsync(string connectionString)
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction(SqlTransaction sqlTransaction)
        {
            _commandRepository.CommitTransaction(sqlTransaction);
        }

        public async Task CommitTransactionAsync(SqlTransaction sqlTransaction)
        {
            await _commandRepository.CommitTransactionAsync(sqlTransaction);
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public void DeleteWithTransaction(object id, SqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWithTransactionAsync(object id, SqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void RollBackTransaction(SqlTransaction sqlTransaction)
        {
            throw new NotImplementedException();
        }

        public Task RollBackTransactionAsync(SqlTransaction sqlTransaction)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _commandRepository.UpdateAsync(entity);
        }

        public void UpdateWithTransaction(TEntity entity, SqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateWithTransactionAsync(TEntity entity, SqlTransaction transaction)
        {
            await _commandRepository.UpdateWithTransactionAsync(entity, transaction);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _commandRepository.UpdateRange(entities);
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _commandRepository.UpdateRangeAsync(entities);
        }

        public void UpdateRangeWithTransaction(IEnumerable<TEntity> entities, SqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeWithTransactionAsync(IEnumerable<TEntity> entities, SqlTransaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
