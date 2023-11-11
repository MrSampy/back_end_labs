using Lab3.Models;

namespace Lab3.Services.Interfaces.Validators
{
    public interface IAccountValidator: IValidator<Account>
    {
        public Task<ValidationResult> ValidateForTopUpBalance(int account_id, decimal amount);
    }
}
