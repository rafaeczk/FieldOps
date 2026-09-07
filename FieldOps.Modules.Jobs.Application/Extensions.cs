using FieldOps.Modules.Jobs.Application.Jobs.Services;
using FieldOps.Modules.Jobs.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Modules.Jobs.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IJobEventMapper, JobEventMapper>();
        services.AddScoped<IJobsModuleApi, JobsModuleApi>();

        return services;
    }
}
