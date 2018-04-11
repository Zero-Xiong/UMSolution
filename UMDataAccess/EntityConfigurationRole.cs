using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UMDomainEntity;

namespace UMDataAccess
{
    public class EntityConfigurationRole : EntityTypeConfiguration<Role>
    {
        public EntityConfigurationRole()
        {
            HasKey(e => e.Id)
                .Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasIndex(e => e.Name).IsUnique();

            Property(e => e.Name).HasMaxLength(100).IsRequired();
        }
    }
}
