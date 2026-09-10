using FieldOps.Modules.Jobs.Contracts;
using FieldOps.Modules.Reports.Application.Reports.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Modules.Reports.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IReportEventMapper, ReportEventMapper>();

        return services;

    }
}
