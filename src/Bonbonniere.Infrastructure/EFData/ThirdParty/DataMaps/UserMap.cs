using Bonbonniere.Core.Models.ThirdParty;
using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Infrastructure.EFData.ThirdParty.DataMaps
{
    public class UserMap : IEntityTypeMap
    {
        public UserMap(ModelBuilder builder)
        {
            builder.Entity<User>(m =>
            {
                m.ToTable("User");
                m.HasKey(t => t.Id);
                m.Property(t => t.Email).HasMaxLength(50).IsRequired();
                m.Property(t => t.Password).HasMaxLength(50).IsRequired();

                m.HasOne(t => t.UserProfile).WithOne(t => t.User).HasForeignKey<UserProfile>(t => t.Id).OnDelete(DeleteBehavior.Cascade); // TODO: deeply study
            });
        }
    }
}
