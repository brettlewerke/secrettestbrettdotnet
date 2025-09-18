using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace secrets_test_dotnet_api.Authentication;

public static class AuthExtensions
{
    public static AuthenticationBuilder AddAuthentication(this WebApplicationBuilder builder, IConfiguration config)
    {
        var defaultScheme = config.GetValue<string>("DefaultScheme") ?? "Bearer";
        var returnValue = builder.Services.AddAuthentication((o) => { o.DefaultChallengeScheme = defaultScheme; });

        builder.Services.AddAuthorization(options => {
            var builder = new AuthorizationPolicyBuilder();
            config.Bind(builder);
            builder.RequireAuthenticatedUser();
            options.DefaultPolicy = builder.Build();
        });

        return returnValue;
    }
}
