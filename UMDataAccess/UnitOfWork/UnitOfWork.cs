using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace UMDataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private readonly ApplicationDbContext context;

        public DbContext Context
        {
            get
            {
                return context;
            }
        }

        public UnitOfWork()
        {
            if (context == null)
                context = new ApplicationDbContext();
        }

        public UnitOfWork(string connectionString)
        {
            if (context == null)
                context = new ApplicationDbContext(connectionString);
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    //context = null;
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
