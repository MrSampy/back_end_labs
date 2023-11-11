using Lab3.Models;

namespace Lab3.Services.Interfaces.Services
{
    public interface IAccountService: ICrud<Account>
    {
        public Task<Account> TopUpBalance(int account_id, decimal amount);
    }
}
