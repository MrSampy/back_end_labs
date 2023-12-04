using Lab4.Models;
namespace Lab4.Services.Interfaces.Validators
{
    public interface IValidator<TModel> where TModel : BaseModel
    {
        public Task<ValidationResult> ValidateForAdd(TModel model);
        public Task<ValidationResult> ValidateIdAsync(int id);

    }
}
