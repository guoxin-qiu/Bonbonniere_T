using Bonbonniere.Core.Enums;
using Bonbonniere.Core.Models;
using Bonbonniere.Core.Models.Bonbonniere.BookStore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bonbonniere.Infrastructure.EFData
{
    public static class BonbonniereContextInitializer
    {
        public static void Initialize(this BonbonniereContext context)
        {
            context.Database.EnsureCreated();
            DateTime now = DateTime.Now;

            #region Initialize Book
            if (!context.Set<Category>().Any())
            {
                var bookCategoryList = new List<Category>
                {
                    new Category
                    {
                        Name = "ASP.NET",
                        Books = new List<BookInfo>
                        {
                            new BookInfo{Title = "Pro ASP.NET MVC 5", Language = Language.English },
                            new BookInfo{Title = "Professional ASP.NET MVC 4", Language = Language.English },
                            new BookInfo{Title = "Beginning ASP.NET 4.5 in C#", Language = Language.English }
                        }
                    },
                    new Category
                    {
                        Name = "Design Pattern",
                        Books = new List<BookInfo>
                        {
                            new BookInfo{Title = "Professional ASP.NET Design Patterns", Language = Language.English },
                            new BookInfo{Title = "Domain-Driven Design", Language = Language.English }
                        }
                    },
                    new Category{ Name = "ASP.NET CORE"},
                    new Category{ Name = "HTML&CSS"},
                    new Category{ Name = "Javascript"},
                    new Category
                    {
                        Name = "Automation Test",
                        Books = new List<BookInfo>
                        {
                            new BookInfo{Title = "The Art of Unit Testing", Language = Language.English }
                        }
                    }
                };

                //var bookList = new List<BookInfo>
                //{
                //    new BookInfo{Title = "Pro ASP.NET MVC 5", Category = bookCategoryList.First(t=>t.Name=="ASP.NET"), Language = Language.English },
                //    new BookInfo{Title = "Professional ASP.NET MVC 4", Category = bookCategoryList.First(t=>t.Name=="ASP.NET"), Language = Language.English },
                //    new BookInfo{Title = "Beginning ASP.NET 4.5 in C#", Category = bookCategoryList.First(t=>t.Name=="ASP.NET"), Language = Language.English },
                //    new BookInfo{Title = "Professional ASP.NET Design Patterns", Category = bookCategoryList.First(t=>t.Name=="Design Patten"), Language = Language.English },
                //    new BookInfo{Title = "Domain-Driven Design", Category = bookCategoryList.First(t=>t.Name=="Design Patten"), Language = Language.English },
                //    new BookInfo{Title = "The Art of Unit Testing", Category = bookCategoryList.First(t=>t.Name=="Automation Test"), Language = Language.English }
                //};

                context.AddRange(bookCategoryList);
                context.SaveChanges();
            }
            #endregion Initialize Book
        }
    }
}
