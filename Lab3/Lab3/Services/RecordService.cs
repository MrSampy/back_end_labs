using Lab3.Database.UnitOfWork;
using Lab3.Models;
using Lab3.Services.Interfaces.Services;
using Lab3.Services.Interfaces.Validators;

namespace Lab3.Services
{
    public class RecordService: IRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecordValidator _validator;

        public RecordService(IUnitOfWork unitOfWork, IRecordValidator validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<IEnumerable<Record>> GetAllAsync()
        {
            return await _unitOfWork.RecordRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Record>> GetFilteredAsync(int? user_id, int? category_id)
        {
            var validationResult = await _validator.ValidateForFiltered(user_id, category_id);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            var result = await GetAllAsync();

            if (user_id != null)
            {
                result = result.Where(x => x.UserId.Equals(user_id)).ToList();
            }
            if (category_id != null)
            {
                result = result.Where(x => x.CategoryId.Equals(category_id)).ToList();
            }

            return result;
        }
        public async Task<Record> GetByIdAsync(int id)
        {
            var validationResult = await _validator.ValidateIdAsync(id);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            return await _unitOfWork.RecordRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Record model)
        {
            model.Id = 0;
            var validationResult = await _validator.ValidateForAdd(model);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }

            var user = await _unitOfWork.UserRepository.GetByIdAsync(model.UserId);
            var account = user.Account;
            account!.Balance -= model.Amount;
            if(account.Balance < 0) 
            {
                throw new Exception("Not enough money in your balance");
            }
            await _unitOfWork.AccountRepository.UpdateAsync(account);

            await _unitOfWork.RecordRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Record> DeleteAsync(int modelId)
        {
            var validationResult = await _validator.ValidateIdAsync(modelId);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            var entity = await _unitOfWork.RecordRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
            return entity;
        }
    }
}
