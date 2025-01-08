using Autofac.Features.Indexed;
using hotelier_core_app.Domain.Helpers;
using hotelier_core_app.Domain.Queries.Interface;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace hotelier_core_app.Domain.Queries.Implementation
{

    public class DBQueryRepository<TEntity> : IDBQueryRepository<TEntity>, IBaseQueryRepository<TEntity> where TEntity : class
    {
        private readonly IDBQueryRepository<TEntity> _queryRepository;

        private readonly IConfiguration _configuration;

        public DBQueryRepository(IIndex<DBProvider, IDBQueryRepository<TEntity>> queryRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            if (!Enum.TryParse<DBProvider>(_configuration.GetValue<string>("AppSettings:OrmType"), ignoreCase: true, out var result))
            {
                result = DBProvider.SQL_Dapper;
            }

            _queryRepository = queryRepository[result];
        }

        public TEntity Find(object id, string connectionString)
        {
            throw new NotImplementedException();
        }

        public TEntity Find(object id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindAsync(object id, string connectionString)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> FindAsync(object id)
        {
            return await _queryRepository.FindAsync(id);
        }

        public IEnumerable<TEntity> GetAll(string connectionString)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync(string connectionString)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _queryRepository.GetAllAsync();
        }

        public IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate, string connectionString)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate, string connectionString)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _queryRepository.GetByAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetByWithLimitAsync(int limit, Expression<Func<TEntity, bool>> predicate)
        {
            return await _queryRepository.GetByWithLimitAsync(limit, predicate);
        }

        public async Task<IEnumerable<TEntity>> GetByIncludesAsync<T1>(Expression<Func<TEntity, bool>> predicate, Func<TEntity, T1, TEntity> map, params Expression<Func<TEntity, object>>[] includes)
        {
            return await _queryRepository.GetByIncludesAsync(predicate, map, includes);
        }

        public async Task<IEnumerable<TEntity>> GetByIncludesWithLimitAsync<T1>(int limit, Expression<Func<TEntity, bool>> predicate, Func<TEntity, T1, TEntity> map, params Expression<Func<TEntity, object>>[] includes)
        {
            return await _queryRepository.GetByIncludesWithLimitAsync(limit, predicate, map, includes);
        }

        public async Task<TEntity> GetByDefaultIncludesAsync<T1>(Expression<Func<TEntity, bool>> predicate, Func<TEntity, T1, TEntity> map, params Expression<Func<TEntity, object>>[] includes)
        {
            return await _queryRepository.GetByDefaultIncludesAsync(predicate, map, includes);
        }

        public TEntity GetByDefault(Expression<Func<TEntity, bool>> predicate, string connectionString)
        {
            throw new NotImplementedException();
        }

        public TEntity GetByDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _queryRepository.GetByDefault(predicate);
        }

        public Task<TEntity> GetByDefaultAsync(Expression<Func<TEntity, bool>> predicate, string connectionString)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetByDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _queryRepository.GetByDefaultAsync(predicate);
        }

        public bool IsExist(Expression<Func<TEntity, bool>> predicate, string connectionString)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate, string connectionString)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _queryRepository.IsExistAsync(predicate);
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _queryRepository.GetCountAsync(predicate);
        }

        public async Task<int> GetNextValueInSequenceAsync(string sequenceName)
        {
            return await _queryRepository.GetNextValueInSequenceAsync(sequenceName);
        }

        public async Task<int> GetNextValueInSequenceAsync(string sequenceName, string connectionString)
        {
            return await _queryRepository.GetNextValueInSequenceAsync(sequenceName, connectionString);
        }
    }
}
