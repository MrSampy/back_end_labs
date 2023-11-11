using Lab3.Models;

namespace Lab3.Services.Interfaces.Validators
{
    public interface IRecordValidator : IValidator<Record>
    {
        public Task<ValidationResult> ValidateForFiltered(int? user_id, int? category_id);
    }
}
