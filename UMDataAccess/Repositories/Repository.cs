using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace UMDataAccess
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
    {

        private readonly IDbSet<TEntity> dbSet;

        public Repository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            dbSet = unitOfWork.Context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }

        public TEntity Get(TKey id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationtoken)
        {
            return await dbSet.ToListAsync<TEntity>(cancellationtoken);
        }
    }
}
