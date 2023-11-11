using Lab3.Database.UnitOfWork;
using Lab3.Models;
using Lab3.Services.Interfaces.Services;
using Lab3.Services.Interfaces.Validators;

namespace Lab3.Services
{
    public class CategoryService: ICrud<Category>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Category> _validator;
        public CategoryService(IUnitOfWork unitOfWork, IValidator<Category> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _unitOfWork.CategoryRepository.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var validationResult = await _validator.ValidateIdAsync(id);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            return await _unitOfWork.CategoryRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Category model)
        {
            model.Id = 0;
            var validationResult = await _validator.ValidateForAdd(model);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            await _unitOfWork.CategoryRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Category> DeleteAsync(int modelId)
        {
            var validationResult = await _validator.ValidateIdAsync(modelId);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            var entity = await _unitOfWork.CategoryRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
            return entity;
        }
    }
}
