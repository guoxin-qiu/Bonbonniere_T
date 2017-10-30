using Bonbonniere.Core.Models.Bonbonniere.BookStore;
using Bonbonniere.Infrastructure.EFData;
using Bonbonniere.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bonbonniere.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly BonbonniereContext _context;

        public BookService(BonbonniereContext context)
        {
            _context = context;
        }

        public Category CreateCategory(string categoryName)
        {
            var newCategory = new Category { Name = categoryName };
            _context.Add(newCategory);
            _context.SaveChanges();
            return newCategory;
        }

        public Category DeleteCategory(int categoryId)
        {
            var category = _context.Categories.SingleOrDefault(t => t.IsActive && t.Id == categoryId);
            if(category == null)
            {
                throw new Exception($"Category can not be found with id {categoryId}");
            }
            category.IsActive = false;
            _context.Update(category);
            _context.SaveChanges();
            return category;
        }

        public Category GetCategory(int categoryId)
        {
            return _context.Categories.Include(t=>t.Books).FirstOrDefault(t => t.IsActive && t.Id == categoryId);
        }

        public List<Category> GetCategoryList()
        {
            return _context.Categories.Include(t => t.Books).Where(t => t.IsActive).ToList();
        }

        public Category UpdateCategory(int categoryId, string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                throw new ArgumentNullException("categoryName");
            }

            var category = _context.Categories.SingleOrDefault(t => t.IsActive && t.Id == categoryId);
            if (category == null)
            {
                throw new Exception($"Category can not be found with id {categoryId}");
            }
            category.Name = categoryName;
            _context.Update(category);
            _context.SaveChanges();
            return category;
        }
    }
}
