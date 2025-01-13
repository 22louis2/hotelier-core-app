using Autofac.Features.Indexed;
using hotelier_core_app.Domain.Commands.Interface;
using hotelier_core_app.Domain.Helpers;
using hotelier_core_app.Domain.Queries.Implementation;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace hotelier_core_app.Domain.Commands.Implementation
{

    public class DBCommandRepository<TEntity> : IDBCommandRepository<TEntity>, IBaseCommandRepository<TEntity> where TEntity : class
    {
        private readonly IIndex<DBProvider, IDBCommandRepository<TEntity>> _commandRepositories;
        private IDBCommandRepository<TEntity> _commandRepository;

        private readonly IConfiguration _configuration;

        public DBCommandRepository(IIndex<DBProvider, IDBCommandRepository<TEntity>> commandRepositories, IConfiguration configuration)
        {
            _commandRepositories = commandRepositories;
            _configuration = configuration;
            if (!Enum.TryParse<DBProvider>(_configuration.GetValue<string>("AppSettings:OrmType"), ignoreCase: true, out var result))
            {
                result = DBProvider.SQL_EFCore;
            }

            _commandRepository = commandRepositories[result];
        }

        public void SwitchProvider(DBProvider provider)
        {
            if (!_commandRepositories.TryGetValue(provider, out var newRepository))
            {
                throw new InvalidOperationException($"Provider {provider} is not registered.");
            }

            _commandRepository = newRepository;
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
            _commandRepository.AddRange(entity);
        }

        public async Task AddRangeAsync(List<TEntity> entity)
        {
            await _commandRepository.AddRangeAsync(entity);
        }

        public void AddRangeWithTransaction(List<TEntity> entity, NpgsqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeWithTransactionAsync(List<TEntity> entity, NpgsqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void AddWithTransaction(TEntity entity, NpgsqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public async Task AddWithTransactionAsync(TEntity entity, NpgsqlTransaction transaction)
        {
            await _commandRepository.AddWithTransactionAsync(entity, transaction);
        }

        public NpgsqlTransaction BeginTransaction()
        {
            return _commandRepository.BeginTransaction();
        }

        public NpgsqlTransaction BeginTransaction(string connectionString)
        {
            throw new NotImplementedException();
        }

        public Task<NpgsqlTransaction> BeginTransactionAsync()
        {
            return _commandRepository.BeginTransactionAsync();
        }

        public Task<NpgsqlTransaction> BeginTransactionAsync(string connectionString)
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction(NpgsqlTransaction sqlTransaction)
        {
            _commandRepository.CommitTransaction(sqlTransaction);
        }

        public async Task CommitTransactionAsync(NpgsqlTransaction sqlTransaction)
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

        public void DeleteWithTransaction(object id, NpgsqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWithTransactionAsync(object id, NpgsqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void RollBackTransaction(NpgsqlTransaction sqlTransaction)
        {
            throw new NotImplementedException();
        }

        public Task RollBackTransactionAsync(NpgsqlTransaction sqlTransaction)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            return _commandRepository.Save();
        }

        public async Task<int> SaveAsync()
        {
            return await _commandRepository.SaveAsync();
        }

        public void Update(TEntity entity)
        {
            _commandRepository.Update(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _commandRepository.UpdateAsync(entity);
        }

        public void UpdateWithTransaction(TEntity entity, NpgsqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateWithTransactionAsync(TEntity entity, NpgsqlTransaction transaction)
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

        public void UpdateRangeWithTransaction(IEnumerable<TEntity> entities, NpgsqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeWithTransactionAsync(IEnumerable<TEntity> entities, NpgsqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            _commandRepository.Delete(entity);
        }

        public void DeleteRange(List<TEntity> entity)
        {
            _commandRepository.DeleteRange(entity);
        }

        public void AttachEntity(TEntity entity)
        {
            _commandRepository.AttachEntity(entity);
        }
    }
}
