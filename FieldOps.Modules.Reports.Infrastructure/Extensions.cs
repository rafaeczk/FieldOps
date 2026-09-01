using FieldOps.Modules.Reports.Infrastructure.EF.Repositories;
using FieldOps.Modules.Reports.Application.Common;
using FieldOps.Modules.Reports.Domain.Reports.Repositories;
using FieldOps.Shared.Infrastructure.Messages;
using FieldOps.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;
using FieldOps.Modules.Reports.Application.Reports.Repositories;

namespace FieldOps.Modules.Reports.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMediatRRequestHandlers(typeof(Application.Extensions));
        services.AddPostgres<ReportsDbContext>();

        services.AddScoped<IReportsUnitOfWork, ReportsUnitOfWork>();


        services.AddScoped<IReportsWriteRepository, ReportsWriteRepository>();
        services.AddScoped<IReportsReadRepository, ReportsReadRepository>();

        return services;
    }
}
