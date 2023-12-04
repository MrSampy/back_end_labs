using Lab4.Database.UnitOfWork;
using Lab4.Models;
using Lab4.Services.Interfaces.Services;
using Lab4.Services.Interfaces.Validators;
using Lab4.Services.Validators;

namespace Lab4.Services
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserValidatorr _validator;

        public UserService(IUnitOfWork unitOfWork, IUserValidatorr validator)
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
            model.Password = SecurePasswordHasher.Hash(model.Password);
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

        public async Task<bool> LogIn(User user)
        {
            var validationResult = await _validator.ValidateForLogIn(user);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            var userToLogIn = (await _unitOfWork.UserRepository.GetAllAsync()).First(x=>x.Name.Equals(user.Name));
            return SecurePasswordHasher.Verify(user.Password, userToLogIn.Password);
        }
    }
}
