using FieldOps.Modules.Jobs.Application.Common;
using FieldOps.Modules.Jobs.Application.Jobs.Repositories;
using FieldOps.Modules.Jobs.Contracts.Events;
using FieldOps.Modules.Jobs.Domain.Jobs.Repositories;
using FieldOps.Modules.Jobs.Domain.Outbox;
using FieldOps.Modules.Jobs.Infrastructure.EF;
using FieldOps.Modules.Jobs.Infrastructure.EF.Repositories;
using FieldOps.Shared.Infrastructure.Events;
using FieldOps.Shared.Infrastructure.Kernel;
using FieldOps.Shared.Infrastructure.Messages;
using FieldOps.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FieldOps.Modules.Jobs.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMediatRRequestHandlers(typeof(Application.Extensions));
        services.AddDomainEventHandlers(typeof(Application.Extensions));

        services.AddPostgres<JobsDbContext>();

        services.AddScoped<IJobsUnitOfWork, JobsUnitOfWork>();

        services.AddScoped<IJobsRepository, JobsRepository>();
        services.AddScoped<IJobsReadRepository, JobsReadRepository>();
        services.AddScoped<IOutboxMessagesRepository, OutboxMessagesRepository>();

        services.AddHostedService(sp
            => new OutboxProcessorWorker<IOutboxMessagesRepository>(
                scopeFactory: sp.GetRequiredService<IServiceScopeFactory>(),
                moduleName: "Jobs",
                typeMapping: new()
                {
                    { "JobAdded", typeof(JobAdded) },
                    { "JobStatusChanged", typeof(JobStatusChanged) }
                },
                logger: sp.GetRequiredService<ILogger<OutboxProcessorWorker<IOutboxMessagesRepository>>>()));

        return services;
    }
}
