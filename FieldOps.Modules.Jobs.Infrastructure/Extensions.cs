using FieldOps.Shared.Infrastructure.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Modules.Jobs.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMediatRHandlers(typeof(Application.Extensions));

        return services;
    }
}
