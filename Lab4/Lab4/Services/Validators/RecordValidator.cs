using Lab4.Database.UnitOfWork;
using Lab4.Models;
using Lab4.Services.Interfaces.Validators;

namespace Lab4.Services.Validators
{
    public class RecordValidator : IRecordValidator
    {
        protected readonly IUnitOfWork unitOfWork;
        public RecordValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ValidationResult> ValidateForAdd(Record model)
        {
            var result = new ValidationResult()
            {
                IsValid = true,
                Messages = new List<string>()
            };

            if (model.Amount < 0)
            {
                result.IsValid = false;
                result.Messages.Add("Amount must be positive");
            }
            if (model.Amount > 1000000)
            {
                result.IsValid = false;
                result.Messages.Add("Amount must be less than 1000000");
            }

            if (model.Date >= DateTime.Today) 
            {
                result.IsValid = false;
                result.Messages.Add("Date must be less than today");
            }

            var existingUser = await unitOfWork.UserRepository.GetByIdAsync(model.UserId);
            if (existingUser == null)
            {
                result.IsValid = false;
                result.Messages.Add("User not found");
            }
            if(existingUser != null) 
            {
                var existingAccount = (await unitOfWork.AccountRepository.GetAllAsync()).FirstOrDefault(x=>x.UserId == existingUser.Id);
                if (existingAccount == null)
                {
                    result.IsValid = false;
                    result.Messages.Add("Account not found");
                }
            }
            var existingCategory = await unitOfWork.CategoryRepository.GetByIdAsync(model.CategoryId);
            if (existingCategory == null)
            {
                result.IsValid = false;
                result.Messages.Add("Category not found");
            }

            return result;
        }

        public async Task<ValidationResult> ValidateForFiltered(int? user_id, int? category_id)
        {
            var result = new ValidationResult()
            {
                IsValid = true,
                Messages = new List<string>()
            };

            if (user_id == null && category_id == null)
            {
                result.IsValid = false;
                result.Messages.Add("Both user_id and category_id parameters are required");
            }

            if (user_id != null) 
            {
                var existingUser = await unitOfWork.UserRepository.GetByIdAsync(user_id.Value);
                if (existingUser == null)
                {
                    result.IsValid = false;
                    result.Messages.Add("User not found");
                }
            }

            if (category_id != null) 
            {
                var existingCategory = await unitOfWork.CategoryRepository.GetByIdAsync(category_id.Value);
                if (existingCategory == null)
                {
                    result.IsValid = false;
                    result.Messages.Add("Category not found");
                }
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

            var existingRecord = await unitOfWork.RecordRepository.GetByIdAsync(id);
            if (existingRecord == null)
            {
                result.IsValid = false;
                result.Messages.Add("Record not found");
            }

            return result;
        }
    }
}
