using hotelier_core_app.Domain.Commands.Interface;
using hotelier_core_app.Domain.Helpers;
using hotelier_core_app.Migrations;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace hotelier_core_app.Domain.Commands.Implementation
{
    public class EFCoreCommandRepository<TEntity> : IDBCommandRepository<TEntity>, IBaseCommandRepository<TEntity> where TEntity : class
    {
        private AppDbContext _context;
        private DbSet<TEntity> _dbSet;

        public EFCoreCommandRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();

        }

        public object Add(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public async Task<object> AddAsync(TEntity entity)
        {
            var valueTask = await _dbSet.AddAsync(entity);
            return valueTask.Entity;
        }

        public void AddRange(List<TEntity> entity)
        {
            _dbSet.AddRange(entity);
        }

        public async Task AddRangeAsync(List<TEntity> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public void AddRangeWithTransaction(List<TEntity> entity, NpgsqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeWithTransactionAsync(List<TEntity> entity, NpgsqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public object AddSoft(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<object> AddSoftAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void AddWithTransaction(TEntity entity, NpgsqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task AddWithTransactionAsync(TEntity entity, NpgsqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public NpgsqlTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public NpgsqlTransaction BeginTransaction(string connectionString)
        {
            throw new NotImplementedException();
        }

        public Task<NpgsqlTransaction> BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task<NpgsqlTransaction> BeginTransactionAsync(string connectionString)
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction(NpgsqlTransaction sqlTransaction)
        {
            throw new NotImplementedException();
        }

        public Task CommitTransactionAsync(NpgsqlTransaction sqlTransaction)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _dbSet.Remove(entity);
        }

        public void DeleteRange(List<TEntity> entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _dbSet.RemoveRange(entity);
        }

        public void AttachEntity(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _dbSet.Attach(entity);
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
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void UpdateRangeWithTransaction(IEnumerable<TEntity> entities, NpgsqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeWithTransactionAsync(IEnumerable<TEntity> entities, NpgsqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void UpdateWithTransaction(TEntity entity, NpgsqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task UpdateWithTransactionAsync(TEntity entity, NpgsqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void SwitchProvider(DBProvider provider)
        {
        }
    }
}
