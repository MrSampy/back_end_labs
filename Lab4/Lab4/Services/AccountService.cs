using Lab4.Database.UnitOfWork;
using Lab4.Models;
using Lab4.Services.Interfaces.Services;
using Lab4.Services.Interfaces.Validators;

namespace Lab4.Services
{
    public class AccountService: IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountValidator _validator;

        public AccountService(IUnitOfWork unitOfWork,IAccountValidator validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _unitOfWork.AccountRepository.GetAllAsync();
        }
        public async Task<Account> TopUpBalance(int account_id, decimal amount)
        {
            var validationResult = await _validator.ValidateForTopUpBalance(account_id, amount);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }

            var account = await _unitOfWork.AccountRepository.GetByIdAsync(account_id);
            account.Balance += amount;
            await _unitOfWork.SaveAsync();
            return account;
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            var validationResult = await _validator.ValidateIdAsync(id);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            return await _unitOfWork.AccountRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Account model)
        {
            model.Id = 0;
            var validationResult = await _validator.ValidateForAdd(model);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            await _unitOfWork.AccountRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Account> DeleteAsync(int modelId)
        {
            var validationResult = await _validator.ValidateIdAsync(modelId);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            var entity = await _unitOfWork.AccountRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
            return entity;
        }

    }
}
