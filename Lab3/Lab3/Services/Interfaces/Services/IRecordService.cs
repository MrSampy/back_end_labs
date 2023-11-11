using Lab3.Models;

namespace Lab3.Services.Interfaces.Services
{
    public interface IRecordService: ICrud<Record>
    {
        public Task<IEnumerable<Record>> GetFilteredAsync(int? user_id, int? category_id);
    }
}
