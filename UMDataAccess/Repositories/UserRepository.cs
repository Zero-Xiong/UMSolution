using System;
using System.Data.Entity;
using UMDomainEntity;

namespace UMDataAccess.Repositories
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {

        private readonly IDbSet<User> dbSet;

        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            dbSet = unitOfWork.Context.Set<User>();
        }
    }
}
