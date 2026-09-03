using FieldOps.Modules.Operators.Contracts;
using FieldOps.Modules.Operators.Contracts.Events;
using FieldOps.Modules.Operators.Core.DAL;
using FieldOps.Modules.Operators.Core.DAL.Repositories;
using FieldOps.Modules.Operators.Core.Repositories;
using FieldOps.Modules.Operators.Core.Services;
using FieldOps.Shared.Infrastructure.Events;
using FieldOps.Shared.Infrastructure.Messages;
using FieldOps.Shared.Infrastructure.Postgres;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FieldOps.Modules.Operators.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<OperatorDbContext>();
        services.AddScoped<IOperatorUnitOfWork, OperatorUnitOfWork>();

        services.AddMediatRNotificationHandlers(typeof(ModuleMarker));
        services.AddMediatRRequestHandlers(typeof(ModuleMarker));

        services.AddScoped<IOperatorRepository, OperatorRepository>();
        services.AddScoped<IOutboxMessagesRepository, OutboxMessagesRepository>();

        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<ModuleMarker>());

        services.AddHostedService(sp
            => new OutboxProcessorWorker<IOutboxMessagesRepository>(
                scopeFactory: sp.GetRequiredService<IServiceScopeFactory>(),
                moduleName: "Operators",
                typeMapping: new()
                {
                    { "OperatorCreated", typeof(OperatorCreated) },
                    { "OperatorDeleted", typeof(OperatorDeleted) }
                },
                logger: sp.GetRequiredService<ILogger<OutboxProcessorWorker<IOutboxMessagesRepository>>>()));

        services.AddScoped<IOperatorService, OperatorService>();

        services.AddScoped<IOperatorsModuleApi, OperatorsModuleApi>();

        services.AddValidatorsFromAssemblyContaining<ModuleMarker>();

        return services;
    }
}

internal class ModuleMarker { }
