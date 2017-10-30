using Bonbonniere.Core.Models.Bonbonniere.BookStore;
using System.Collections.Generic;

namespace Bonbonniere.Services.Interfaces
{
    public interface IBookService
    {
        List<Category> GetCategoryList();
        Category GetCategory(int categoryId);
        Category CreateCategory(string categoryName);
        Category UpdateCategory(int categoryId, string categoryName);
        Category DeleteCategory(int categoryId);
    }
}
