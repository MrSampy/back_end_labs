using Lab4.Database.UnitOfWork;
using Lab4.Models;
using Lab4.Services.Interfaces.Validators;

namespace Lab4.Services.Validators
{
    public class UserValidator : IUserValidatorr
    {
        protected readonly IUnitOfWork unitOfWork;
        public UserValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private Task<ValidationResult> ValidateEntity(User model) 
        {
            var result = new ValidationResult()
            {
                IsValid = true,
                Messages = new List<string>()
            };

            if (string.IsNullOrEmpty(model.Name))
            {
                result.IsValid = false;
                result.Messages.Add("Name is required");
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                result.IsValid = false;
                result.Messages.Add("Password is required");
            }

            return Task.FromResult(result);
        }

        public async Task<ValidationResult> ValidateForAdd(User model)
        {
            var result = await ValidateEntity(model);

            var existingUser = (await unitOfWork.UserRepository.GetAllAsync()).FirstOrDefault(u => u.Name == model.Name);
            if (existingUser != null)
            {
                result.IsValid = false;
                result.Messages.Add("User with this name already exists");
            }

            return result;
        }

        public async Task<ValidationResult> ValidateForLogIn(User user)
        {
            var result = await ValidateEntity(user);

            var existingUser = (await unitOfWork.UserRepository.GetAllAsync()).FirstOrDefault(u => u.Name == user.Name);
            if (existingUser == null)
            {
                result.IsValid = false;
                result.Messages.Add("User not found");
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

            var existingUser = await unitOfWork.UserRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                result.IsValid = false;
                result.Messages.Add("User not found");
            }

            return result;  
        }
    }
}
