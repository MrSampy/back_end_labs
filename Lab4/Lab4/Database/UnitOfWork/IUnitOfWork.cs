using Lab4.Models;

namespace Lab4.Database.UnitOfWork
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
