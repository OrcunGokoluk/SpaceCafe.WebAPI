using Mapster;
using MapsterMapper;
using System.Reflection;

namespace SpaceCafe.WebAPI.Common.Mapping;

public static class DependencyInjection
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        //TypeAdapterConfig bir Mapster classıdır
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly()); //Iregister uygulayan sınıfları otomatik tarar

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>(); // I mapper gördüğün yere Service Mapper Injectle
        return services;
    }
}
