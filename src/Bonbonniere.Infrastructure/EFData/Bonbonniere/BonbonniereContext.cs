using Bonbonniere.Core.Models.Bonbonniere.BookStore;
using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Infrastructure.EFData
{
    public class BonbonniereContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookInfo> BookInfos { get; set; }

        public BonbonniereContext(DbContextOptions<BonbonniereContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.RegisterEntities("Bonbonniere.Core", "Bonbonniere.Core.Models.Bonbonniere");
            modelBuilder.RegisterMaps("Bonbonniere.Infrastructure.EFData.Bonbonniere.DataMaps");
        }
    }
}
