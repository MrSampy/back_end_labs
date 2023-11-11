using Lab3.Models;

namespace Lab3.Services.Interfaces.Services
{
    public interface ICrud<TModel> where TModel : class
    {
        Task<IEnumerable<TModel>> GetAllAsync();

        Task<TModel> GetByIdAsync(int id);

        Task AddAsync(TModel model);

        Task<TModel> DeleteAsync(int modelId);
    }
}
