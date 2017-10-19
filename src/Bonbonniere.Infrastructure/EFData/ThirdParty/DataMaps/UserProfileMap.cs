using Bonbonniere.Core.Models.ThirdParty;
using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Infrastructure.EFData.ThirdParty.DataMaps
{
    public class UserProfileMap : IEntityTypeMap
    {
        public UserProfileMap(ModelBuilder builder)
        {
            builder.Entity<UserProfile>(m =>
            {
                m.ToTable("UserProfile");
                m.HasKey(t => t.Id);
                m.Property(t => t.Gender).IsRequired();
                m.Property(t => t.UserName).HasMaxLength(50).IsRequired();
                m.Property(t => t.Address).HasMaxLength(200);
            });
        }
    }
}
