using Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Lab2.Controllers
{
    [ApiController]
    public class UserController
    {
        private static List<User> users = new List<User>() {
            new User() { Id = 1, Name = "Reobert" },
            new User() { Id = 2, Name = "Jhon" },
            new User() { Id = 3, Name = "Tom" },
            new User() { Id = 4, Name = "Serhiy" },
            new User() { Id = 5, Name = "GaryLocal" },
        };

        public UserController() 
        {            
        }

        //GET: /users
        [Route("users")]
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return new ObjectResult(users);
        }

        //GET: /user/id
        [Route("user/{user_id:int}")]
        [HttpGet]
        public ActionResult<User> GetUserById(int user_id)
        {
            return new ObjectResult(users.FirstOrDefault(x=>x.Id.Equals(user_id)));
        }
        //DELETE: /user/user_id
        [Route("user/{user_id:int}")]
        [HttpDelete]
        public ActionResult<User> DeleteUserById(int user_id)
        {
            return new ObjectResult(users.Remove(users.FirstOrDefault(x => x.Id.Equals(user_id))));
        }
        // POST: /user
        [Route("user")]
        [HttpPost]
        public ActionResult<User> AddUser([FromBody] User user)
        {
            users.Add(user);
            return new OkObjectResult(user);
        }
    }
}
