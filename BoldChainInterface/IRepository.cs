using System.Linq.Expressions;

namespace BoldChainBackendAPI.BoldChainInterface
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T item);
        Task<T?> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
        void Update(T item);
        void Delete(T item);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

    }
}
