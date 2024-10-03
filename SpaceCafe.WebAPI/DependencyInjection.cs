using SpaceCafe.WebAPI.Common.Mapping;

namespace SpaceCafe.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMappings();
        return services;
    }
}
