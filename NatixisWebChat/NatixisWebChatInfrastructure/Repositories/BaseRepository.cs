namespace NatixisWebChatInfrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using NatixisWebChatInfrastructure.Context;
    using NatixisWebChatInfrastructure.Repositories.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        /// <summary>The database set</summary>
        protected readonly DbSet<T> _dbSet;

        /// <summary>The context</summary>
        private readonly NatixisDbContext _context;

        /// <summary>The constructor</summary>
        /// <param name="context">The context</param>
        public BaseRepository(NatixisDbContext context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }   
    }
}
