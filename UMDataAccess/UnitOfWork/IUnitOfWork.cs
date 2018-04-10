using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace UMDataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }

        void Commit();

        Task CommitAsync(CancellationToken cancellationtoken = default(CancellationToken));
    }
}
