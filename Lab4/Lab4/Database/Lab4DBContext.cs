using Lab4.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Database
{
    public class Lab4DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public Lab4DBContext(DbContextOptions<Lab4DBContext> options) 
            : base(options) 
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(a => a.Balance)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Record>()
                .Property(r => r.Amount)
                .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<Account>().HasKey(a => a.Id);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Records)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Record>()
                .HasOne(r => r.Category)
                .WithMany(c => c.Records)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Records)
                .WithOne(r => r.Category)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Account)
                .WithOne(a => a.User)
                .HasForeignKey<Account>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade); 
            
        }
    }
}
