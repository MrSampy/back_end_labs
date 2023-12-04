using Lab4.Models;

namespace Lab4.Services.Interfaces.Services
{
    public interface ICrud<TModel> where TModel : class
    {
        Task<IEnumerable<TModel>> GetAllAsync();

        Task<TModel> GetByIdAsync(int id);

        Task AddAsync(TModel model);

        Task<TModel> DeleteAsync(int modelId);
    }
}
