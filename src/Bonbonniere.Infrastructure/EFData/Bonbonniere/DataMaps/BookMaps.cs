using Bonbonniere.Core.Models.Bonbonniere.BookStore;
using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Infrastructure.EFData.Bonbonniere.DataMaps
{
    public class CategoryMap : IEntityTypeMap
    {
        public CategoryMap(ModelBuilder builder)
        {
            builder.Entity<Category>(m =>
            {
                m.ToTable("B_Category");
                m.HasKey(t => t.Id);
                m.Property(t => t.Name).HasMaxLength(100).IsRequired();
            });
        }
    }

    public class BookInfoMap: IEntityTypeMap
    {
        public BookInfoMap(ModelBuilder builder)
        {
            builder.Entity<BookInfo>(m =>
            {
                m.ToTable("B_BookInfo");
                m.HasKey(t => t.Id);
                m.Property(t => t.Title).HasMaxLength(100).IsRequired();
                m.Property(t => t.Author).HasMaxLength(100);
                m.Property(t => t.ISBN10).HasMaxLength(100);
                m.Property(t => t.ISBN13).HasMaxLength(100);
                m.Property(t => t.Publisher).HasMaxLength(100);
                m.Property(t => t.CoverImageUrl).HasMaxLength(100);
                m.Property(t => t.Price).HasColumnType(typeName: "decimal(8,2)");

                m.HasOne(t => t.Category).WithMany(t => t.Books).HasForeignKey(t => t.CategoryId);
            });
        }
    }
}
