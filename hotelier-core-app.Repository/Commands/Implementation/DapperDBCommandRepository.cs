using hotelier_core_app.Domain.Commands.Interface;
using hotelier_core_app.Domain.Executers;
using hotelier_core_app.Domain.SqlGenerator;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Reflection;

namespace hotelier_core_app.Domain.Commands.Implementation
{

    public class DapperDBCommandRepository<TEntity> : IDBCommandRepository<TEntity>, IBaseCommandRepository<TEntity> where TEntity : class
    {
        private readonly IConfiguration _configuration;

        private readonly IExecuters _executers;

        private readonly string _connectionString;

        private readonly ISqlGenerator<TEntity> _sqlGenerator;

        private const int UniqueIndexExceptionNumber = 2601;

        private SqlConnection _connection;

        private SqlTransaction _transaction;

        public DapperDBCommandRepository(IConfiguration configuration, IExecuters executers, ISqlGenerator<TEntity> sqlGenerator)
        {
            _configuration = configuration;
            _executers = executers;
            _sqlGenerator = sqlGenerator;
            _connectionString = _configuration.GetConnectionString("DbConnectionString");
        }

        public object AddSoft(TEntity entity)
        {
            IDictionary<string, object> obj = (IDictionary<string, object>)(object)_executers.ExecuteCommand<TEntity>(_connectionString, _sqlGenerator.GetInsertQuery(entity).GetSql(), _sqlGenerator.GetInsertQueryParams(entity));
            Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            foreach (KeyValuePair<string, object> item in obj)
            {
                dictionary[item.Key] = item.Value;
            }

            return dictionary["ID"];
        }

        public async Task<object> AddSoftAsync(TEntity entity)
        {
            object id = null;
            try
            {
                dynamic val = await _executers.ExecuteCommandAsync<TEntity>(_connectionString, _sqlGenerator.GetInsertQuery(entity).GetSql(), _sqlGenerator.GetInsertQueryParams(entity));
                if ((object)val != null)
                {
                    IDictionary<string, object> obj = (IDictionary<string, object>)val;
                    Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    foreach (KeyValuePair<string, object> item in obj)
                    {
                        dictionary[item.Key] = item.Value;
                    }

                    id = dictionary["ID"];
                }

                return id;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2601)
                {
                    SqlQuery uniqueSelectQuery = _sqlGenerator.GetUniqueSelectQuery(entity);
                    TEntity obj2 = await _executers.ExecuteSingleReaderAsync<TEntity>(_connectionString, uniqueSelectQuery.GetSql(), uniqueSelectQuery.Param);
                    PropertyInfo[] properties = typeof(TEntity).GetProperties();
                    foreach (PropertyInfo propertyInfo in properties)
                    {
                        if (propertyInfo.Name.Equals("id", StringComparison.OrdinalIgnoreCase))
                        {
                            id = propertyInfo.GetValue(obj2).ToString();
                            break;
                        }
                    }

                    return id;
                }

                throw;
            }
        }

        public object Add(TEntity entity)
        {
            try
            {
                object obj = _executers.ExecuteCommand<TEntity>(_connectionString, _sqlGenerator.GetInsertQuery(entity).GetSql(), _sqlGenerator.GetInsertQueryParams(entity));
                if (obj != null)
                {
                    IDictionary<string, object> obj2 = (IDictionary<string, object>)obj;
                    Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    foreach (KeyValuePair<string, object> item in obj2)
                    {
                        dictionary[item.Key] = item.Value;
                    }

                    return dictionary["ID"];
                }

                return null;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                throw;
            }
        }

        public async Task<object> AddAsync(TEntity entity)
        {
            try
            {
                dynamic val = await _executers.ExecuteCommandAsync<TEntity>(_connectionString, _sqlGenerator.GetInsertQuery(entity).GetSql(), _sqlGenerator.GetInsertQueryParams(entity));
                if ((object)val != null)
                {
                    IDictionary<string, object> obj = (IDictionary<string, object>)val;
                    Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    foreach (KeyValuePair<string, object> item in obj)
                    {
                        dictionary[item.Key] = item.Value;
                    }

                    return dictionary["ID"];
                }

                return null;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                throw;
            }
        }

        public void AddRange(List<TEntity> entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            try
            {
                SqlQuery bulkInsertQuery = _sqlGenerator.GetBulkInsertQuery(entities);
                await _executers.ExecuteCommandAsync<TEntity>(_connectionString, bulkInsertQuery.GetSql(), bulkInsertQuery.Param);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                throw;
            }
        }

        public void AddRangeWithTransaction(List<TEntity> entity, SqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public async Task AddRangeWithTransactionAsync(List<TEntity> entity, SqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void AddWithTransaction(TEntity entity, SqlTransaction transaction)
        {
            try
            {
                _executers.ExecuteCommand<TEntity>(_sqlGenerator.GetInsertQuery(entity).GetSql(), _sqlGenerator.GetInsertQueryParams(entity), transaction);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                throw;
            }
        }

        public async Task AddWithTransactionAsync(TEntity entity, SqlTransaction transaction)
        {
            try
            {
                await _executers.ExecuteCommandAsync<TEntity>(_sqlGenerator.GetInsertQuery(entity).GetSql(), _sqlGenerator.GetInsertQueryParams(entity), transaction);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                throw;
            }
        }

        public SqlTransaction BeginTransaction()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            return _connection.BeginTransaction();
        }

        public SqlTransaction BeginTransaction(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            return _connection.BeginTransaction();
        }

        public async Task<SqlTransaction> BeginTransactionAsync()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            return _connection.BeginTransaction();
        }

        public async Task<SqlTransaction> BeginTransactionAsync(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            return _connection.BeginTransaction();
        }

        public void CommitTransaction(SqlTransaction sqlTransaction)
        {
            try
            {
                sqlTransaction.Commit();
            }
            catch
            {
                sqlTransaction.Rollback();
                throw;
            }
            finally
            {
                sqlTransaction.Dispose();
            }
        }

        public async Task CommitTransactionAsync(SqlTransaction sqlTransaction)
        {
            try
            {
                await sqlTransaction.CommitAsync();
            }
            catch
            {
                await sqlTransaction.RollbackAsync();
                throw;
            }
            finally
            {
                await sqlTransaction.DisposeAsync();
            }
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public void DeleteWithTransaction(object id, SqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteWithTransactionAsync(object id, SqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void RollBackTransaction(SqlTransaction sqlTransaction)
        {
            throw new NotImplementedException();
        }

        public async Task RollBackTransactionAsync(SqlTransaction sqlTransaction)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            try
            {
                SqlQuery updateQuery = _sqlGenerator.GetUpdateQuery(entity);
                await _executers.ExecuteCommandAsync<TEntity>(_connectionString, updateQuery.GetSql(), updateQuery.Param);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateWithTransaction(TEntity entity, SqlTransaction transaction)
        {
            try
            {
                SqlQuery updateQuery = _sqlGenerator.GetUpdateQuery(entity);
                _executers.ExecuteCommand<TEntity>(_sqlGenerator.GetUpdateQuery(entity).GetSql(), updateQuery.Param, transaction);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                throw;
            }
        }

        public async Task UpdateWithTransactionAsync(TEntity entity, SqlTransaction transaction)
        {
            try
            {
                SqlQuery updateQuery = _sqlGenerator.GetUpdateQuery(entity);
                await _executers.ExecuteCommandAsync<TEntity>(_sqlGenerator.GetUpdateQuery(entity).GetSql(), updateQuery.Param, transaction);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                throw;
            }
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            try
            {
                SqlQuery bulkUpdateQuery = _sqlGenerator.GetBulkUpdateQuery(entities);
                _executers.ExecuteCommand<TEntity>(_connectionString, bulkUpdateQuery.GetSql(), bulkUpdateQuery.Param);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                SqlQuery bulkUpdateQuery = _sqlGenerator.GetBulkUpdateQuery(entities);
                await _executers.ExecuteCommandAsync<TEntity>(_connectionString, bulkUpdateQuery.GetSql(), bulkUpdateQuery.Param);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveAsync()
        {
            throw new NotImplementedException();
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
