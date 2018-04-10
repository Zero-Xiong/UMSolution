using System;

namespace UMDomainEntity
{
    public class UserRole
    {
        /// <summary>
        /// Guid
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Guid
        /// </summary>
        public Guid RoleId { get; set; }

        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}
