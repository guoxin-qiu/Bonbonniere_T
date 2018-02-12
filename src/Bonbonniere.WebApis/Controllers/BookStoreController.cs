using Bonbonniere.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Bonbonniere.WebApis.Controllers
{
    [Route("api/category")]
    public class BookStoreController : Controller
    {
        private readonly ILogger<BookStoreController> _logger;
        private readonly IBookService _bookService;

        public BookStoreController(
            ILogger<BookStoreController> logger,
            IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var list = _bookService.GetCategoryList();

            var categoryListView = _bookService.GetCategoryList()
                .Select(category => new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Books = category.Books?.Select(b => new BookViewModel { Id = b.Id, Title = b.Title }).ToList()
                });

            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _bookService.GetCategory(id);
            if (category == null)
            {
                _logger.LogInformation($"Can not find category with id {id}");
                return NotFound();
            }

            var categoryView = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Books = category.Books?.Select(b => new BookViewModel { Id = b.Id, Title = b.Title }).ToList()
            };

            return Ok(categoryView);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CategoryCreation category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            if (category.CategoryName == "Category")
            {
                ModelState.AddModelError("CategoryName", "Category name can not be 'Category'");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCategory = _bookService.CreateCategory(category.CategoryName);

            return CreatedAtRoute("GetCategory", new { id = newCategory.Id }, category);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryModification category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            if (category.CategoryName == "Category")
            {
                ModelState.AddModelError("CategoryName", "Category name can not be 'Category'");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCategory = _bookService.UpdateCategory(id, category.CategoryName);

            return CreatedAtRoute("GetCategory", new { id = newCategory.Id }, category);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]JsonPatchDocument<CategoryModification> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var model = _bookService.GetCategory(id);
            if (model == null)
            {
                return NotFound();
            }

            var toPatch = new CategoryModification
            {
                CategoryName = model.Name,
            };

            patchDoc.ApplyTo(toPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (toPatch.CategoryName == "Category")
            {
                ModelState.AddModelError("CategoryName", "Category name can not be 'Category'");
            }

            TryValidateModel(toPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _bookService.UpdateCategory(id, toPatch.CategoryName);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookService.DeleteCategory(id);
            return NoContent();
        }

        [HttpGet("{categoryId}/books")]
        public IActionResult GetBooks(int categoryId)
        {
            var category = _bookService.GetCategory(categoryId);
            if (category == null)
            {
                return NotFound();
            }

            var books = category.Books?.Select(t => new BookViewModel { Id = t.Id, Title = t.Title });

            return Ok(books);
        }

        [HttpGet("{categoryId}/books/{id}")]
        public IActionResult GetBook(int categoryId, int id)
        {
            var category = _bookService.GetCategory(categoryId);
            if (category == null)
            {
                return NotFound();
            }

            var book = category.Books?.FirstOrDefault(t => t.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(new BookViewModel { Id = book.Id, Title = book.Title });
        }
    }

    #region 
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

    #endregion
}
