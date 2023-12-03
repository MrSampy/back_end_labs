using Lab3.Database;
using Lab3.Database.UnitOfWork;
using Lab3.Models;
using Lab3.Services;
using Lab3.Services.Interfaces.Services;
using Lab3.Services.Interfaces.Validators;
using Lab3.Services.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Newtonsoft.Json;
using System.Net.Mime;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Lab3
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            #region Database
            string connection = Configuration.GetConnectionString("PostgreSQLConnection")!;

            var optionsBuilder = new DbContextOptionsBuilder<Lab3DBContext>().UseNpgsql(connection, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());

            var options = optionsBuilder.Options;

            var context = new Lab3DBContext(options);

            context.Database.EnsureCreated();

            SeedData(context);

            var unitOfWork = new UnitOfWork(context);

            services.AddDbContext<Lab3DBContext>(options =>
            {
                options.UseNpgsql(connection, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
            });            

            services.AddSingleton<IUnitOfWork>(x => unitOfWork);

            #endregion

            #region Validators

            services.AddTransient<IRecordValidator, RecordValidator>();

            services.AddTransient<IValidator<User>, UserValidator>();

            services.AddTransient<IValidator<Category>, CategoryValidator>();

            services.AddTransient<IAccountValidator, AccountValidator>();

            #endregion

            #region Services

            services.AddTransient<IRecordService, RecordService>();

            services.AddTransient<IAccountService, AccountService>();

            services.AddTransient<ICrud<User>, UserService>();

            services.AddTransient<ICrud<Category>, CategoryService>();

            #endregion

            services.AddControllers();

            services.AddHealthChecks();

            services.AddEndpointsApiExplorer();
        }

        protected void SeedData(Lab3DBContext context) 
        {
            context.Users.AddRange(
                new User { Name = "Користувач 1" },
                new User { Name = "Користувач 2" },
                new User { Name = "Користувач 3" },
                new User { Name = "Користувач 4" },
                new User { Name = "Користувач 5" },
                new User { Name = "Користувач 6" },
                new User { Name = "Користувач 7" },
                new User { Name = "Користувач 8" }
            );
            context.SaveChanges();
            context.Categories.AddRange(
                new Category { Name = "Категорія 1" },
                new Category { Name = "Категорія 2" },
                new Category { Name = "Категорія 3" },
                new Category { Name = "Категорія 4" },
                new Category { Name = "Категорія 5" },
                new Category { Name = "Категорія 6" }
            );
            context.SaveChanges();
            context.Records.AddRange(
                new Record { UserId = 1, CategoryId = 1, Date = DateTime.Now.ToUniversalTime(), Amount = 200 },
                new Record { UserId = 1, CategoryId = 2, Date = DateTime.Now.ToUniversalTime(), Amount = 150 },
                new Record { UserId = 2, CategoryId = 1, Date = DateTime.Now.ToUniversalTime(), Amount = 300 },
                new Record { UserId = 3, CategoryId = 3, Date = DateTime.Now.ToUniversalTime(), Amount = 250 },
                new Record { UserId = 4, CategoryId = 2, Date = DateTime.Now.ToUniversalTime(), Amount = 175 },
                new Record { UserId = 5, CategoryId = 1, Date = DateTime.Now.ToUniversalTime(), Amount = 350 },
                new Record { UserId = 6, CategoryId = 4, Date = DateTime.Now.ToUniversalTime(), Amount = 420 },
                new Record { UserId = 7, CategoryId = 3, Date = DateTime.Now.ToUniversalTime(), Amount = 275 }
            );
            context.SaveChanges();
            context.Accounts.AddRange(
                new Account { UserId = 1, Balance = 1000 },
                new Account { UserId = 2, Balance = 500 },
                new Account { UserId = 3, Balance = 750 },
                new Account { UserId = 4, Balance = 1200 },
                new Account { UserId = 5, Balance = 800 },
                new Account { UserId = 6, Balance = 950 },
                new Account { UserId = 7, Balance = 1100 },
                new Account { UserId = 8, Balance = 700 }
            );
            context.SaveChanges();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();            

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthcheck", new HealthCheckOptions
                {
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonConvert.SerializeObject(
                            new
                            {
                                status = report.Status.ToString(),
                                date = DateTime.Now
                            }, Formatting.Indented);
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });
            });

        }

    }
}
