using System.Linq.Expressions;

namespace hotelier_core_app.Domain.Queries.Interface
{
    public interface IBaseQueryRepository<TEntity> where TEntity : class
    {
        TEntity Find(object id);

        Task<TEntity> FindAsync(object id);

        IEnumerable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync();

        IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetByWithLimitAsync(int limit, Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetByIncludesAsync<T1>(Expression<Func<TEntity, bool>> predicate, Func<TEntity, T1, TEntity> map, params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> GetByIncludesWithLimitAsync<T1>(int limit, Expression<Func<TEntity, bool>> predicate, Func<TEntity, T1, TEntity> map, params Expression<Func<TEntity, object>>[] includes);

        TEntity GetByDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetByDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetByDefaultIncludesAsync<T1>(Expression<Func<TEntity, bool>> predicate, Func<TEntity, T1, TEntity> map, params Expression<Func<TEntity, object>>[] includes);

        bool IsExist(Expression<Func<TEntity, bool>> predicate);

        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate);

        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
