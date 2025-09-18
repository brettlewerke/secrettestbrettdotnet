using Microsoft.OpenApi.Models;

namespace secrets_test_dotnet_api.Swagger

{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerWithAuthentication(this IServiceCollection services, IConfiguration config, ILogger logger)
        {

            services.AddSwaggerGen(options =>
            {



            });

            return services;
        }

        // Insert Swagger & its UI into the pipeline, prefill some auth info and title.
        public static WebApplication UseSwaggerWithOptions(this WebApplication app, IConfiguration config)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "secrets_test_dotnet_api Swagger UI";

            });
            return app;
        }

    }
}