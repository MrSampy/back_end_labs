using Lab4.Database;
using Lab4.Database.UnitOfWork;
using Lab4.Models;
using Lab4.Services;
using Lab4.Services.Interfaces.Services;
using Lab4.Services.Interfaces.Validators;
using Lab4.Services.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Lab4
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

            var optionsBuilder = new DbContextOptionsBuilder<Lab4DBContext>().UseNpgsql(connection, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());

            var options = optionsBuilder.Options;

            using (var context = new Lab4DBContext(options))
            {
                var canConnect = false;

                try
                {
                    canConnect = context.Database.CanConnect();

                    context.Users.FirstOrDefault();
                    context.Categories.FirstOrDefault();
                    context.Records.FirstOrDefault();
                    context.Accounts.FirstOrDefault();
                }
                catch
                {
                    canConnect = false;
                }

                if (!canConnect)
                {
                    context.Database.EnsureCreated();
                    SeedData(context);
                }
            }
            
            var unitOfWork = new UnitOfWork(new Lab4DBContext(options));

            services.AddDbContext<Lab4DBContext>(options =>
            {
                options.UseNpgsql(connection, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
            });   

            services.AddSingleton<IUnitOfWork>(x => unitOfWork);

            #endregion

            #region Validators

            services.AddTransient<IRecordValidator, RecordValidator>();

            services.AddTransient<IUserValidatorr, UserValidator>();

            services.AddTransient<IValidator<Category>, CategoryValidator>();

            services.AddTransient<IAccountValidator, AccountValidator>();

            #endregion

            #region Services

            services.AddTransient<IRecordService, RecordService>();

            services.AddTransient<IAccountService, AccountService>();

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<ICrud<Category>, CategoryService>();

            #endregion

            services.AddControllers();

            services.AddHealthChecks();

            services.AddEndpointsApiExplorer();

            #region Authentication
            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme);

                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) 
                };
            });
            #endregion
        }

        protected void SeedData(Lab4DBContext context) 
        {
            context.Users.AddRange(
                new User { Name = "1", Password = SecurePasswordHasher.Hash("1") }, 
                new User { Name = "2", Password = SecurePasswordHasher.Hash("123") },
                new User { Name = "3", Password = SecurePasswordHasher.Hash("qwe") },
                new User { Name = "4", Password = SecurePasswordHasher.Hash("asdfds") },
                new User { Name = "5", Password = SecurePasswordHasher.Hash("21") },
                new User { Name = "6", Password = SecurePasswordHasher.Hash("532") },
                new User { Name = "7", Password = SecurePasswordHasher.Hash("2346") },
                new User { Name = "8", Password = SecurePasswordHasher.Hash("75") }
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

            app.UseAuthorization();

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
