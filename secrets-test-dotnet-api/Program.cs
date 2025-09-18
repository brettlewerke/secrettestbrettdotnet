using secrets_test_dotnet_api.Services;
using secrets_test_dotnet_api.Authentication;
using secrets_test_dotnet_api.Observability;
using secrets_test_dotnet_api.ErrorHandling;
using secrets_test_dotnet_api.Configuration;
using secrets_test_dotnet_api.Swagger;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// create basic console logging to provide visibility into configuration setup
var logger = ObservabilityExtension.GetBasicConsoleLogger<Program>();
builder.UseAzureAppConfiguration(logger);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithAuthentication(builder.Configuration.GetSection("Swagger"), logger);

builder.Services.AddHttpClient();
builder.AddDependencyChecks();
builder.AddAuthentication(builder.Configuration.GetSection("Authentication"));

// Add business services
builder.Services.AddSingleton<IInsightService, InsightService>();
builder.Services.Configure<InsightServiceConfig>(builder.Configuration.GetSection(InsightServiceConfig.Config));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithOptions(builder.Configuration);
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
//Enable dev-certs and local HTTPS debugging first: ``app.UseHttpsRedirection();``
app.UseAuthorization();
app.MapControllers();
app.MapHealthCheckEndpoint();

await app.RunAsync();
