using Lab3.Database.UnitOfWork;
using Lab3.Models;
using Lab3.Services.Interfaces.Services;
using Lab3.Services.Interfaces.Validators;

namespace Lab3.Services
{
    public class UserService:ICrud<User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<User> _validator;

        public UserService(IUnitOfWork unitOfWork, IValidator<User> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var validationResult = await _validator.ValidateIdAsync(id);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            return await _unitOfWork.UserRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(User model)
        {
            model.Id = 0;
            var validationResult = await _validator.ValidateForAdd(model);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            await _unitOfWork.UserRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task<User> DeleteAsync(int modelId)
        {
            var validationResult = await _validator.ValidateIdAsync(modelId);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            var entity = await _unitOfWork.UserRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
            return entity;
        }
    }
}
