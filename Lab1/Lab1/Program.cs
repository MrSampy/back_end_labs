using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHealthChecks();
var app = builder.Build();


app.MapHealthChecks("/healthcheck", new HealthCheckOptions
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

app.MapGet("/", () => "Hello user!");



app.Run();

