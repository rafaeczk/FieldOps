using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Modules.Jobs.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
