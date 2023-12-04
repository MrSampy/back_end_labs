using Lab4.Models;

namespace Lab4.Services.Interfaces.Validators
{
    public interface IAccountValidator: IValidator<Account>
    {
        public Task<ValidationResult> ValidateForTopUpBalance(int account_id, decimal amount);
    }
}
