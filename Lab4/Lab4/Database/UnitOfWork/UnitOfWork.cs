using Lab4.Models;

namespace Lab4.Database.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly Lab4DBContext Context;

        private Repository<User> _userRepository;
        private Repository<Account> _accountRepository;
        private Repository<Record> _recordRepository;
        private Repository<Category> _categoryRepository;

        public Repository<User> UserRepository => _userRepository ??= new Repository<User>(Context);
        public Repository<Account> AccountRepository => _accountRepository ??= new Repository<Account>(Context);
        public Repository<Record> RecordRepository => _recordRepository ??= new Repository<Record>(Context);
        public Repository<Category> CategoryRepository => _categoryRepository ??= new Repository<Category>(Context);

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public UnitOfWork(Lab4DBContext context)
        {
            Context = context;
        }

    }
}
