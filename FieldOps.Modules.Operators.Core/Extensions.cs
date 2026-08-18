using FieldOps.Modules.Operators.Core.DAL;
using FieldOps.Modules.Operators.Core.DAL.Repositories;
using FieldOps.Modules.Operators.Core.Repositories;
using FieldOps.Modules.Operators.Core.Services;
using FieldOps.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Modules.Operators.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<OperatorDbContext>();
        services.AddScoped<IOperatorRepository, OperatorRepository>();
        services.AddScoped<IOperatorService, OperatorService>();

        return services;
    }
}
