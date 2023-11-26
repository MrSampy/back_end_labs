using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Net.Mime;

namespace Lab2
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
            services.AddControllers();

            services.AddHealthChecks();

            services.AddEndpointsApiExplorer();

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
