using Lab4.Models;

namespace Lab4.Services.Interfaces.Services
{
    public interface IUserService: ICrud<User>
    {
        public Task<bool> LogIn(User user);
    }
}
