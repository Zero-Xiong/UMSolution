using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMDataAccess
{
    public class ApplicationDbContext : DbContext
    {
        //public virtual IDbSet<User> Users { set; get; }
        //public virtual IDbSet<Role> Roles { set; get; }
        //public virtual IDbSet<UserRole> UserRoles { get; set; }
        //public virtual IDbSet<UserToken> UserTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
