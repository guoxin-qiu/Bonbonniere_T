using System.Collections.Generic;

namespace Bonbonniere.Core.Models.Bonbonniere.BookStore
{
    public class Category : EntityBase,IAggregateRoot
    {
        public string Name { get; set; }
        
        public List<BookInfo> Books { get; set; }
        //public Category Parent { get; set; }
        //public List<Category> Children { get; set; }
    }
}
