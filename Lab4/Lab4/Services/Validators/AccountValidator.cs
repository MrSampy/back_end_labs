using Lab4.Database.UnitOfWork;
using Lab4.Models;
using Lab4.Services.Interfaces.Validators;

namespace Lab4.Services.Validators
{
    public class AccountValidator : IAccountValidator
    {
        protected readonly IUnitOfWork unitOfWork;
        public AccountValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ValidationResult> ValidateForAdd(Account model)
        {
            var result = new ValidationResult()
            {
                IsValid = true,
                Messages = new List<string>()
            };

            if(model.Balance < 0)
            {
                result.IsValid = false;
                result.Messages.Add("Balance must be positive");
            }
            if(model.Balance > 1000000)
            {
                result.IsValid = false;
                result.Messages.Add("Balance must be less than 1000000");
            }

            var existingUser = await unitOfWork.UserRepository.GetByIdAsync(model.UserId);
            if (existingUser == null)
            {
                result.IsValid = false;
                result.Messages.Add("User not found");
            }
            else if(existingUser!.Account != null) 
            {
                result.IsValid = false;
                result.Messages.Add("User already has an account");
            }

            return result;
        }

        public async Task<ValidationResult> ValidateForTopUpBalance(int account_id, decimal amount)
        {
            var result = new ValidationResult()
            {
                IsValid = true,
                Messages = new List<string>()
            };

            var existingAccount = await unitOfWork.AccountRepository.GetByIdAsync(account_id);
            if (existingAccount == null)
            {
                result.IsValid = false;
                result.Messages.Add("Account not found");
            }
            else if (existingAccount!.Balance + amount > 1000000)
            {
                result.IsValid = false;
                result.Messages.Add("Balance must be less than 1000000");
            }
            if (amount < 0)
            {
                result.IsValid = false;
                result.Messages.Add("Amount must be positive");
            }


            return result;
        }

        public async Task<ValidationResult> ValidateIdAsync(int id)
        {
            var result = new ValidationResult()
            {
                IsValid = true,
                Messages = new List<string>()
            };

            var existingAccount = await unitOfWork.AccountRepository.GetByIdAsync(id);
            if (existingAccount == null)
            {
                result.IsValid = false;
                result.Messages.Add("Account not found");
            }

            return result;
        }
    }
}
