using BoldChainBackendAPI.BoldChainData;
using BoldChainBackendAPI.BoldChainInterface;

namespace BoldChainBackendAPI.BoldChainService
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly BoldChainContext _context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        public UnitOfWork(BoldChainContext context)
        {
            _context = context;
        }
        public IRepository<T> Repository<T>() where T : class
        {
            if(_repositories.ContainsKey(typeof(T)))
            {
                return (IRepository<T>)_repositories[typeof(T)];
            }
            var repoInstance = new Repository<T>(_context);
            _repositories[typeof(T)] = repoInstance;
            return repoInstance;
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
