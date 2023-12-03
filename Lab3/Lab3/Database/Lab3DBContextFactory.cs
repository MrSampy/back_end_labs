using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Lab3.Database
{
    public class Lab3DBContextFactory: IDesignTimeDbContextFactory<Lab3DBContext>
    {
        public Lab3DBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json")
                           .Build();

            var builder = new DbContextOptionsBuilder<Lab3DBContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseNpgsql(connectionString, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());

            return new Lab3DBContext(builder.Options);
        }
    }
}
