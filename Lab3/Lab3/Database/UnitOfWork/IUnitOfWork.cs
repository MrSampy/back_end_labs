using Lab3.Models;

namespace Lab3.Database.UnitOfWork
{
    public interface IUnitOfWork
    {
        Repository<User> UserRepository { get; }
        Repository<Account> AccountRepository { get; }
        Repository<Record> RecordRepository { get; }
        Repository<Category> CategoryRepository { get; }

        public Task SaveAsync();
    }
}
