
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SpaceCafe.Application.Common.Behaviours;


namespace SpaceCafe.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        //Validation

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(typeof(SpaceCafe.Application.DependencyInjection).Assembly);

        return services;
    }
}
