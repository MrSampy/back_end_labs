using Lab4.Models;

namespace Lab4.Services.Interfaces.Services
{
    public interface IAccountService: ICrud<Account>
    {
        public Task<Account> TopUpBalance(int account_id, decimal amount);
    }
}
