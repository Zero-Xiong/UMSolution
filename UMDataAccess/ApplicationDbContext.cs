using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using UMDomainEntity;

namespace UMDataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public ApplicationDbContext() : base("name=UMSolutionDb")
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public virtual IDbSet<User> Users { set; get; }
        public virtual IDbSet<Role> Roles { set; get; }
        public virtual IDbSet<UserRole> UserRoles { get; set; }
        public virtual IDbSet<UserToken> UserTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new EntityConfigurationUser());
            modelBuilder.Configurations.Add(new EntityConfigurationRole());
            modelBuilder.Configurations.Add(new EntityConfigurationUserRole());
            modelBuilder.Configurations.Add(new EntityConfigurationUserToken());

            base.OnModelCreating(modelBuilder);
        }
    }
}
