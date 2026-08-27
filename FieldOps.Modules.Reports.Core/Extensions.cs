using FieldOps.Modules.Reports.Core.DAL;
using FieldOps.Modules.Reports.Core.DAL.Repositories;
using FieldOps.Modules.Reports.Core.Repositories;
using FieldOps.Modules.Reports.Core.Services;
using FieldOps.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Modules.Reports.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<ReportDbContext>();
        services.AddScoped<IReportUnitOfWork, ReportUnitOfWork>();

        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<IReportService, ReportService>();

        return services;
    }
}

internal class ModuleMarker { }
