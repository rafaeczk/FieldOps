using FieldOps.Modules.Reports.Application.Common;
using FieldOps.Modules.Reports.Application.Reports.Repositories;
using FieldOps.Modules.Reports.Contracts.Events;
using FieldOps.Modules.Reports.Domain.Outbox;
using FieldOps.Modules.Reports.Domain.Reports.Repositories;
using FieldOps.Modules.Reports.Infrastructure.EF.Repositories;
using FieldOps.Modules.Reports.Infrastructure.Repositories;
using FieldOps.Shared.Infrastructure.Events;
using FieldOps.Shared.Infrastructure.Kernel;
using FieldOps.Shared.Infrastructure.Messages;
using FieldOps.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FieldOps.Modules.Reports.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMediatRRequestHandlers(typeof(Application.Extensions));
        services.AddDomainEventHandlers(typeof(Application.Extensions));

        services.AddPostgres<ReportsDbContext>();

        services.AddScoped<IReportsUnitOfWork, ReportsUnitOfWork>();

        services.AddScoped<IReportsWriteRepository, ReportsWriteRepository>();
        services.AddScoped<IReportsReadRepository, ReportsReadRepository>();
        services.AddScoped<IOutboxMessagesRepository, OutboxMessagesRepository>();

        services.AddHostedService(sp
            => new OutboxProcessorWorker<IOutboxMessagesRepository>(
                scopeFactory: sp.GetRequiredService<IServiceScopeFactory>(),
                moduleName: "Reports",
                typeMapping: new()
                {
                    { "ReportAdded", typeof(ReportAdded) },
                    { "ReportAssigneeAdded", typeof(ReportAssigneeAdded) },
                    { "ReportAssigneeRemoved", typeof(ReportAssigneeRemoved) },
                },
                logger: sp.GetRequiredService<ILogger<OutboxProcessorWorker<IOutboxMessagesRepository>>>()));

        return services;
    }
}
