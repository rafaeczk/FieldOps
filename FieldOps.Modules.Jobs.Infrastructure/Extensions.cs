using FieldOps.Modules.Jobs.Application.Common;
using FieldOps.Modules.Jobs.Application.Jobs.Repositories;
using FieldOps.Modules.Jobs.Domain.Jobs.Repositories;
using FieldOps.Modules.Jobs.Infrastructure.EF;
using FieldOps.Modules.Jobs.Infrastructure.EF.Repositories;
using FieldOps.Shared.Infrastructure.Messages;
using FieldOps.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Modules.Jobs.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMediatRRequestHandlers(typeof(Application.Extensions));

        services.AddPostgres<JobsDbContext>();

        services.AddScoped<IJobsUnitOfWork, JobsUnitOfWork>();

        services.AddScoped<IJobsRepository, JobsRepository>();
        services.AddScoped<IJobsReadRepository, JobsReadRepository>();

        return services;
    }
}
