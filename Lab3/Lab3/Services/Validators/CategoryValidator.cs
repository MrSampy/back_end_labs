using Lab3.Database.UnitOfWork;
using Lab3.Models;
using Lab3.Services.Interfaces.Validators;

namespace Lab3.Services.Validators
{
    public class CategoryValidator : IValidator<Category>
    {
        protected readonly IUnitOfWork unitOfWork;
        public CategoryValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<ValidationResult> ValidateForAdd(Category model)
        {
            var result = new ValidationResult()
            {
                IsValid = true,
                Messages = new List<string>()
            };

            if(string.IsNullOrEmpty(model.Name))
            {
                result.IsValid = false;
                result.Messages.Add("Name is required");
            }

            return Task.FromResult(result);
        }

        public async Task<ValidationResult> ValidateIdAsync(int id)
        {
            var result = new ValidationResult()
            {
                IsValid = true,
                Messages = new List<string>()
            };

            var existingCategory = await unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
            {
                result.IsValid = false;
                result.Messages.Add("Category not found");
            }

            return result;
        }
    }
}
