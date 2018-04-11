using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UMDomainEntity;

namespace UMDataAccess
{
    public class EntityConfigurationUser : EntityTypeConfiguration<User>
    {
        public EntityConfigurationUser()
        {
            HasKey(e => e.Id);
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasIndex(e => e.Email).IsUnique();
            Property(e => e.Email).HasMaxLength(200).IsRequired();

            Property(e => e.Password).IsRequired();
        }
    }
}
