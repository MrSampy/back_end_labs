using Lab4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab4.Services;
using Lab4.Services.Interfaces.Services;

namespace Lab4.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController
    {
        private readonly ICrud<Category> categoryService;
        public CategoryController(ICrud<Category> categoryService)
        {
            this.categoryService = categoryService;
        }

        //GET: /category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return new ObjectResult(await categoryService.GetAllAsync());
        }

        //DELETE: /category/id
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Category>> DeleteCategoryById(int id)
        {
            Category result;
            try
            {
                result = await categoryService.DeleteAsync(id);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

            return new ObjectResult(result);
        }

        // POST: /category
        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory([FromBody] Category category)
        {
            try
            {
                await categoryService.AddAsync(category);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            return new ObjectResult(category);
        }
    }
}
