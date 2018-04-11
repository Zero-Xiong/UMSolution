using System.Data.Entity.ModelConfiguration;
using UMDomainEntity;

namespace UMDataAccess
{
    public class EntityConfigurationUserToken : EntityTypeConfiguration<UserToken>
    {
        public EntityConfigurationUserToken()
        {
            HasRequired(ut => ut.User)
                      .WithMany(u => u.UserTokens)
                      .HasForeignKey(ut => ut.UserId);

            Property(ut => ut.RefreshTokenIdHash).HasMaxLength(450).IsRequired();

            Property(ut => ut.RefreshTokenIdHashSource).HasMaxLength(450);
        }
    }
}
