using FieldOps.Modules.Reports.Application.Reports.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Modules.Reports.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IReportEventMapper, ReportEventMapper>();
        services.AddValidatorsFromAssemblyContaining<ModuleMarker>();

        return services;

    }
}

internal class ModuleMarker { }

