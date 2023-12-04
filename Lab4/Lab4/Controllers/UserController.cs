using Lab4.Models;
using Lab4.Services;
using Lab4.Services.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Lab4.Controllers
{
    [ApiController]
    public class UserController
    {
        private readonly ICrud<User> userService;

        public UserController(ICrud<User> userService) 
        {            
            this.userService = userService;
        }

        //GET: /users
        [Route("users")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return new ObjectResult(await userService.GetAllAsync());
        }

        //GET: /user/id
        [Route("user/{user_id:int}")]
        [HttpGet]
        public async Task<ActionResult<User>> GetUserById(int user_id)
        {
            User result;
            try
            {
                result = await userService.GetByIdAsync(user_id);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

            return new ObjectResult(result);
        }
        //DELETE: /user/user_id
        [Route("user/{user_id:int}")]
        [HttpDelete]
        public async Task<ActionResult<User>> DeleteUserById(int user_id)
        {
            User result;
            try
            {
                result = await userService.DeleteAsync(user_id);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

            return new ObjectResult(result);
        }
        // POST: /user
        [Route("user")]
        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] User user)
        {
            try
            {
                await userService.AddAsync(user);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            return new OkObjectResult(user);
        }
    }
}
