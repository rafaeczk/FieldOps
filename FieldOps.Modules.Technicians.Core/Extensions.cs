using FieldOps.Modules.Technicians.Contracts;
using FieldOps.Modules.Technicians.Contracts.Events;
using FieldOps.Modules.Technicians.Core.DAL;
using FieldOps.Modules.Technicians.Core.DAL.Repositories;
using FieldOps.Modules.Technicians.Core.Repositories;
using FieldOps.Modules.Technicians.Core.Services;
using FieldOps.Shared.Infrastructure.Events;
using FieldOps.Shared.Infrastructure.Messages;
using FieldOps.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FieldOps.Modules.Technicians.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddPostgres<TechniciansDbContext>();
            services.AddScoped<ITechnicianUnitOfWork, TechnicianUnitOfWork>();

            services.AddMediatRNotificationHandlers(typeof(ModuleMarker));
            services.AddMediatRRequestHandlers(typeof(ModuleMarker));

            services.AddScoped<ITechnicianRepository, TechnicianRepository>();
            services.AddScoped<IOutboxMessagesRepository, OutboxMessagesRepository>();

            services.AddHostedService(sp
                => new OutboxProcessorWorker<IOutboxMessagesRepository>(
                    scopeFactory: sp.GetRequiredService<IServiceScopeFactory>(),
                    moduleName: "Technicians",
                    typeMapping: new()
                    {
                        { "TechnicianCreated", typeof(TechnicianCreated) },
                        { "TechnicianDeleted", typeof(TechnicianDeleted) }
                    },
                    logger: sp.GetRequiredService<ILogger<OutboxProcessorWorker<IOutboxMessagesRepository>>>()));

            services.AddScoped<ITechnicianService, TechnicianService>();

            services.AddScoped<ITechniciansModuleApi, TechniciansModuleApi>();

            return services;
        }
    }
}

internal class ModuleMarker { }
