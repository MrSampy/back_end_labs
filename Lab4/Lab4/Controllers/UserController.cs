using Lab4.Models;
using Lab4.Services;
using Lab4.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace Lab4.Controllers
{
    //Controller for user handling
    [ApiController]
    public class UserController
    {
        private readonly IUserService userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration) 
        {            
            this.userService = userService;
            _configuration = configuration;
        }
        private string GenerateJwtToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // POST: api/login
        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> LogIn([FromBody] User user)
        {
            bool result;
            try
            {
                result = await userService.LogIn(user);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            if (result)
            {
                var tokenString = GenerateJwtToken();
                return new OkObjectResult(new { token = tokenString });
            }
            return new UnauthorizedObjectResult("Wrong password");
        }

        //GET: /users
        [Authorize]
        [Route("users")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return new ObjectResult(await userService.GetAllAsync());
        }

        //GET: /user/id
        [Authorize]
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
        [Authorize]
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
        [AllowAnonymous]
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
