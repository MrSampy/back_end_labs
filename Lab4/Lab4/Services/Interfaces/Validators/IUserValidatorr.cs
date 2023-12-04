using Lab4.Models;

namespace Lab4.Services.Interfaces.Validators
{
    public interface IUserValidatorr: IValidator<User>
    {
        public Task<ValidationResult> ValidateForLogIn(User user);
    }
}
