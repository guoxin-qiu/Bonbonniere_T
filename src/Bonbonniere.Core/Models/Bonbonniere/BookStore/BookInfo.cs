using Bonbonniere.Core.Enums;

namespace Bonbonniere.Core.Models.Bonbonniere.BookStore
{
    public class BookInfo : EntityBase, IAggregateRoot
    {
        public string Title { get; set; }
        public Language Language { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string ISBN10 { get; set; }
        public string ISBN13 { get; set; }
        public decimal? Price { get; set; }
        public string CoverImageUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
