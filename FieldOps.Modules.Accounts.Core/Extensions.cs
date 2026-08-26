using FieldOps.Modules.Accounts.Core.DAL;
using FieldOps.Modules.Accounts.Core.DAL.Repositories;
using FieldOps.Modules.Accounts.Core.Entities;
using FieldOps.Modules.Accounts.Core.Events.Foreign.OperatorCreated;
using FieldOps.Modules.Accounts.Core.Events.Foreign.OperatorDeleted;
using FieldOps.Modules.Accounts.Core.Events.Foreign.TechnicianCreated;
using FieldOps.Modules.Accounts.Core.Events.Foreign.TechnicianDeleted;
using FieldOps.Modules.Accounts.Core.Repositories;
using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Shared.Abstractions.Events;
using FieldOps.Shared.Infrastructure.Messaging;
using FieldOps.Shared.Infrastructure.Postgres;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Modules.Accounts.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<AccountDbContext>();
        services.AddScoped<IAccountUnitOfWork, AccountUnitOfWork>();

        services.AddHostedService<AccountInitializer>();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddSingleton<IPasswordHasher<Account>, PasswordHasher<Account>>();

        services.AddScoped<IEventHandler<OperatorCreatedEvent>, OperatorCreatedEventHandler>();
        services.AddScoped<IEventHandler<OperatorDeletedEvent>, OperatorDeletedEventHandler>();
        services.AddScoped<IEventHandler<TechnicianDeletedEvent>, TechnicianDeletedEventHandler>();
        services.AddScoped<IEventHandler<TechnicianCreatedEvent>, TechnicianCreatedEventHandler>();

        services.Configure<MessageRegistryOptions>(options =>
        {
            options.BroadcastActionEventTypes.Add(typeof(OperatorCreatedEvent));
            options.BroadcastActionEventTypes.Add(typeof(OperatorDeletedEvent));
            options.BroadcastActionEventTypes.Add(typeof(TechnicianDeletedEvent));
            options.BroadcastActionEventTypes.Add(typeof(TechnicianCreatedEvent));
        });

        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<ModuleMarker>());

        return services;
    }
}

internal class ModuleMarker { }
