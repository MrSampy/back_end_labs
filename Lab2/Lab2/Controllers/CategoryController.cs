using Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController
    {
        private static List<Category> categories = new List<Category>() 
        {
            new Category { Id = 1, Name = "Food" },
            new Category { Id = 2, Name = "Transport" },
            new Category { Id = 3, Name = "Entertainment" },
            new Category { Id = 4, Name = "Health" }
        };

        public CategoryController()
        {
        }

        //GET: /category
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            return new ObjectResult(categories);
        }

        //DELETE: /category/id
        [HttpDelete("{id:int}")]
        public ActionResult<Category> DeleteCategoryById(int id)
        {
            return new ObjectResult(categories.Remove(categories.FirstOrDefault(x => x.Id.Equals(id))));
        }

        // POST: /category
        [HttpPost]
        public ActionResult<Category> AddCategory([FromBody] Category category)
        {
            categories.Add(category);
            return new ObjectResult(category);
        }
    }
}
