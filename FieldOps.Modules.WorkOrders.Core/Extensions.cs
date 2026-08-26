using FieldOps.Modules.WorkOrders.Core.DAL;
using FieldOps.Modules.WorkOrders.Core.DAL.Repositories;
using FieldOps.Modules.WorkOrders.Core.Repositories;
using FieldOps.Modules.WorkOrders.Core.Services;
using FieldOps.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Modules.WorkOrders.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<WorkOrderDbContext>();
        services.AddScoped<IWorkOrderUnitOfWork, WorkOrderUnitOfWork>();

        services.AddScoped<IWorkOrderRepository, WorkOrderRepository>();
        services.AddScoped<IWorkOrderService, WorkOrderService>();

        return services;
    }
}

internal class ModuleMarker { }
