using hotelier_core_app.Domain.Executers;
using hotelier_core_app.Domain.Queries.Interface;
using hotelier_core_app.Domain.SqlGenerator;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace hotelier_core_app.Domain.Queries.Implementation
{
    public class DapperDBQueryRepository<TEntity> : IDBQueryRepository<TEntity>, IBaseQueryRepository<TEntity> where TEntity : class
    {
        private readonly string _connStr;

        private readonly IExecuters _executers;

        private readonly IConfiguration _configuration;

        private readonly ISqlGenerator<TEntity> _sqlGenerator;

        public DapperDBQueryRepository(IConfiguration configuration, IExecuters executers, ISqlGenerator<TEntity> sqlGenerator)
        {
            _configuration = configuration;
            _executers = executers;
            _connStr = _configuration.GetConnectionString("DbConnectionString");
            _sqlGenerator = sqlGenerator;
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
            SqlQuery selectById = _sqlGenerator.GetSelectById(id);
            return await _executers.ExecuteSingleReaderAsync<TEntity>(_connStr, selectById.GetSql(), selectById.Param);
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
            SqlQuery selectAllQuery = _sqlGenerator.GetSelectAllQuery(0);
            return await _executers.ExecuteReaderAsync<TEntity>(_connStr, selectAllQuery.GetSql(), selectAllQuery.Param);
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
            SqlQuery selectQuery = _sqlGenerator.GetSelectQuery(predicate, 0);
            return await _executers.ExecuteReaderAsync<TEntity>(_connStr, selectQuery.GetSql(), selectQuery.Param);
        }

        public async Task<IEnumerable<TEntity>> GetByWithLimitAsync(int limit, Expression<Func<TEntity, bool>> predicate)
        {
            SqlQuery selectQuery = _sqlGenerator.GetSelectQuery(predicate, limit);
            return await _executers.ExecuteReaderAsync<TEntity>(_connStr, selectQuery.GetSql(), selectQuery.Param);
        }

        public async Task<IEnumerable<TEntity>> GetByIncludesAsync<T1>(Expression<Func<TEntity, bool>> predicate, Func<TEntity, T1, TEntity> map, params Expression<Func<TEntity, object>>[] includes)
        {
            SqlQuery selectQuery = _sqlGenerator.GetSelectQuery(predicate, 0, includes);
            return await _executers.ExecuteReaderWithIncludeAsync(_connStr, selectQuery.GetSql(), map, selectQuery.Param);
        }

        public async Task<IEnumerable<TEntity>> GetByIncludesWithLimitAsync<T1>(int limit, Expression<Func<TEntity, bool>> predicate, Func<TEntity, T1, TEntity> map, params Expression<Func<TEntity, object>>[] includes)
        {
            SqlQuery selectQuery = _sqlGenerator.GetSelectQuery(predicate, limit, includes);
            return await _executers.ExecuteReaderWithIncludeAsync(_connStr, selectQuery.GetSql(), map, selectQuery.Param);
        }

        public async Task<TEntity> GetByDefaultIncludesAsync<T1>(Expression<Func<TEntity, bool>> predicate, Func<TEntity, T1, TEntity> map, params Expression<Func<TEntity, object>>[] includes)
        {
            SqlQuery selectQuery = _sqlGenerator.GetSelectQuery(predicate, 1, includes);
            return (await _executers.ExecuteReaderWithIncludeAsync(_connStr, selectQuery.GetSql(), map, selectQuery.Param)).FirstOrDefault();
        }

        public TEntity GetByDefault(Expression<Func<TEntity, bool>> predicate, string connectionString)
        {
            throw new NotImplementedException();
        }

        public TEntity GetByDefault(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetByDefaultAsync(Expression<Func<TEntity, bool>> predicate, string connectionString)
        {
            SqlQuery selectQuery = _sqlGenerator.GetSelectQuery(predicate, 1);
            return await _executers.ExecuteSingleReaderAsync<TEntity>(connectionString, selectQuery.GetSql(), selectQuery.Param);
        }

        public async Task<TEntity> GetByDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            SqlQuery selectQuery = _sqlGenerator.GetSelectQuery(predicate, 1);
            return await _executers.ExecuteSingleReaderAsync<TEntity>(_connStr, selectQuery.GetSql(), selectQuery.Param);
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
            SqlQuery selectQuery = _sqlGenerator.GetSelectQuery(predicate, 1);
            return await _executers.ExecuteSingleReaderAsync<bool>(_connStr, selectQuery.GetSql(), selectQuery.Param);
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            SqlQuery count = _sqlGenerator.GetCount(predicate);
            return await _executers.ExecuteSingleReaderAsync<int>(_connStr, count.GetSql(), count.Param);
        }

        public async Task<int> GetNextValueInSequenceAsync(string sequenceName)
        {
            return (await _executers.ExecuteReaderAsync<int>(_connStr, "SELECT NEXT VALUE FOR " + sequenceName, null)).FirstOrDefault();
        }

        public async Task<int> GetNextValueInSequenceAsync(string sequenceName, string connectionString)
        {
            return (await _executers.ExecuteReaderAsync<int>(connectionString, "SELECT NEXT VALUE FOR " + sequenceName, null)).FirstOrDefault();
        }
    }
}
