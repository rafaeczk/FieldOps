using FieldOps.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Shared.Infrastructure.Events;

internal static class Extensions
{
    internal static IServiceCollection AddEvents(this IServiceCollection services)
    {
        services.AddSingleton<IEventDispatcher, EventDispatcher>();

        return services;
    }
}
