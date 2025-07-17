using BoldChainBackendAPI.BoldChainData;
using BoldChainBackendAPI.BoldChainInterface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BoldChainBackendAPI.BoldChainService
{
    public class Repository<T>:IRepository<T> where T : class
    {
        protected readonly BoldChainContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(BoldChainContext context)
        {
            _context = context;
            _dbSet =context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T?> GetByIdAsync(object Id)
        {
            return await _dbSet.FindAsync(Id);
        }
        public async Task AddAsync(T entity)
        {
            _dbSet.Add(entity);
        }
        public async Task<bool> ExistsAsync(Expression<Func<T,bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
