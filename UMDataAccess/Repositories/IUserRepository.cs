using System;
using UMDomainEntity;

namespace UMDataAccess
{
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}
