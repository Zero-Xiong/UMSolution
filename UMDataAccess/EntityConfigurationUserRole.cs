using System.Data.Entity.ModelConfiguration;
using UMDomainEntity;

namespace UMDataAccess
{
    public class EntityConfigurationUserRole : EntityTypeConfiguration<UserRole>
    {
        public EntityConfigurationUserRole()
        {
            HasKey(e => new { e.UserId, e.RoleId });

            HasIndex(e => e.UserId);

            HasIndex(e => e.RoleId);


            HasRequired(d => d.Role).WithMany(p => p.UserRoles).HasForeignKey(d => d.RoleId);
            HasRequired(d => d.User).WithMany(p => p.UserRoles).HasForeignKey(d => d.UserId);
        }
    }
}
