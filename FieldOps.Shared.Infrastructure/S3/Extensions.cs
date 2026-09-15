using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Shared.Infrastructure.S3;

internal static class Extensions
{
    public static IServiceCollection AddS3(this IServiceCollection services)
    {
        var options = services.GetOptions<S3Options>("S3");
        services.AddSingleton(options);

        return services;
    }
}
