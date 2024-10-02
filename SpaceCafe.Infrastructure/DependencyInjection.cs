using Microsoft.Extensions.DependencyInjection;
using SpaceCafe.Application.Common.Interfaces.Authentication;
using SpaceCafe.Application.Common.Interfaces.Persistance;
using SpaceCafe.Application.Common.Interfaces.Services;
using SpaceCafe.Infrastructure.Authentication;
using SpaceCafe.Infrastructure.Persistance;
using SpaceCafe.Infrastructure.Services;

namespace SpaceCafe.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IUserRepository, UserRepository>();
        return services;
    }
}
