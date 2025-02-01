using hotelier_core_app.Domain.Helpers;
using hotelier_core_app.Domain.Queries.Interface;
using hotelier_core_app.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace hotelier_core_app.Domain.Queries.Implementation
{
    public class EFCoreQueryRepository<TEntity> : IDBQueryRepository<TEntity>, IBaseQueryRepository<TEntity> where TEntity : class
    {
        private AppDbContext _context;
        private DbSet<TEntity> _dbSet;

        public EFCoreQueryRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
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

        public async Task<TEntity?> FindAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IEnumerable<TEntity> GetAll(string connectionString)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync(string connectionString)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public IQueryable<TEntity> GetAllQueryable()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<TEntity> GetAllTrackEntity()
        {
            return _dbSet;
        }

        public IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate, string connectionString)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }

        public Task<IEnumerable<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate, string connectionString)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public TEntity GetByDefault(Expression<Func<TEntity, bool>> predicate, string connectionString)
        {
            throw new NotImplementedException();
        }

        public TEntity? GetByDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public Task<TEntity> GetByDefaultAsync(Expression<Func<TEntity, bool>> predicate, string connectionString)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity?> GetByDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public TEntity? GetByDefaultAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(predicate);
        }

        public async Task<TEntity?> GetByDefaultAsNoTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public Task<TEntity?> GetByDefaultIncludesAsync<T1>(Expression<Func<TEntity, bool>> predicate, Func<TEntity, T1, TEntity> map, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }
        public TEntity? GetByDefaultIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var Query = GetAllIncluding(includeProperties);

            return Query.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> Queryable = _dbSet.AsNoTracking();

            return includeProperties.Aggregate
              (Queryable, (current, includeProperty) => current.Include(includeProperty));
        }

        public IEnumerable<TEntity> AllInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties).ToList();
        }

        public IEnumerable<TEntity> FindByInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var Query = GetAllIncluding(includeProperties);
            IEnumerable<TEntity> results = Query.Where(predicate).ToList();
            return results;
        }

        public IQueryable<TEntity> GetByAllIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> Queryable = _dbSet.AsNoTracking().Where(predicate);

            return includeProperties.Aggregate
              (Queryable, (current, includeProperty) => current.Include(includeProperty));
        }

        public TEntity? FindWithChildInclude(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeMembers)
        {
            DbSet<TEntity> set = _context.Set<TEntity>();
            IQueryable<TEntity> result = includeMembers(set.AsNoTracking());

            return result.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAllByWithChildInclude(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeMembers)
        {
            DbSet<TEntity> set = _context.Set<TEntity>();
            IQueryable<TEntity> result = includeMembers(set.AsNoTracking());

            return result.Where(predicate).AsQueryable<TEntity>();
        }

        public IQueryable<TEntity> GetAllWithChildInclude(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeMembers)
        {
            DbSet<TEntity> set = _context.Set<TEntity>();
            IQueryable<TEntity> result = includeMembers(set.AsNoTracking());

            return result;
        }

        public IQueryable<TEntity> GetRecordUsingStoredProcedure(string storedProcedure, object[] parameter)
        {
            return _dbSet.FromSqlRaw(storedProcedure, parameter);
        }

        public Task<IEnumerable<TEntity>> GetByIncludesAsync<T1>(Expression<Func<TEntity, bool>> predicate, Func<TEntity, T1, TEntity> map, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetByIncludesWithLimitAsync<T1>(int limit, Expression<Func<TEntity, bool>> predicate, Func<TEntity, T1, TEntity> map, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetByWithLimitAsync(int limit, Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNextValueInSequenceAsync(string sequenceName)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNextValueInSequenceAsync(string sequenceName, string connectionString)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(Expression<Func<TEntity, bool>> predicate, string connectionString)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Any(predicate);
        }

        public Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate, string connectionString)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().AnyAsync(predicate);
        }

        public void SwitchProvider(DBProvider provider)
        {
        }
    }
}
