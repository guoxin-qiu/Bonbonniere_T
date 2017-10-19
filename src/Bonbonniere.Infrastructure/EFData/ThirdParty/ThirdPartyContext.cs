using Bonbonniere.Core.Models.ThirdParty;
using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Infrastructure.EFData
{
    public class ThirdPartyContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public ThirdPartyContext(DbContextOptions<ThirdPartyContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.RegisterEntities("Bonbonniere.Core", "Bonbonniere.Core.Models.ThirdParty");
            modelBuilder.RegisterMaps("Bonbonniere.Infrastructure.EFData.ThirdParty.DataMaps");
        }
    }
}
