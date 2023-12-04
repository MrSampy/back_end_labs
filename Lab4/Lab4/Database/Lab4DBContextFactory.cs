using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Lab4.Database
{
    public class Lab4DBContextFactory: IDesignTimeDbContextFactory<Lab4DBContext>
    {
        public Lab4DBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json")
                           .Build();

            var builder = new DbContextOptionsBuilder<Lab4DBContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseNpgsql(connectionString, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());

            return new Lab4DBContext(builder.Options);
        }
    }
}
