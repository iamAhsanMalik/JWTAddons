using JWTAddons.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JWTAddons;
public static class JWTAddonsServices
{

    public static IServiceCollection AddJWTAddons(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.AddScoped(typeof(ISocialLoginValidators), typeof(SocialLoginValidators));
        return services;
    }
}
