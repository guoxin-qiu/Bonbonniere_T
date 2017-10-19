using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bonbonniere.Website.ViewModels.BookStore
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookViewModel> Books { get; set; }
    }

    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class CategoryCreation
    {
        [Display(Name = "Category name")]
        [Required(ErrorMessage = "{0} is required")]
        public string CategoryName { get; set; }
    }

    public class CategoryModification
    {
        [Display(Name = "Category name")]
        [Required(ErrorMessage = "{0} is required")]
        public string CategoryName { get; set; }
    }
}
