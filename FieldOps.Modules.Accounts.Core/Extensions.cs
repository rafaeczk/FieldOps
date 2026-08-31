using FieldOps.Modules.Accounts.Contracts.Events;
using FieldOps.Modules.Accounts.Core.DAL;
using FieldOps.Modules.Accounts.Core.DAL.Repositories;
using FieldOps.Modules.Accounts.Core.Entities;
using FieldOps.Modules.Accounts.Core.Repositories;
using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Shared.Infrastructure.Events;
using FieldOps.Shared.Infrastructure.Messages;
using FieldOps.Shared.Infrastructure.Postgres;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FieldOps.Modules.Accounts.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<AccountDbContext>();
        services.AddScoped<IAccountUnitOfWork, AccountUnitOfWork>();

        services.AddMediatRNotificationHandlers(typeof(ModuleMarker));
        services.AddMediatRRequestHandlers(typeof(ModuleMarker));

        services.AddHostedService<AccountInitializer>();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IOutboxMessagesRepository, OutboxMessagesRepository>();

        services.AddHostedService(sp
            => new OutboxProcessorWorker<IOutboxMessagesRepository>(
                scopeFactory: sp.GetRequiredService<IServiceScopeFactory>(),
                moduleName: "Operators",
                typeMapping: new()
                {
                    { "AccountCreated", typeof(AccountCreated) },
                },
                logger: sp.GetRequiredService<ILogger<OutboxProcessorWorker<IOutboxMessagesRepository>>>()));

        services.AddTransient<IIdentityService, IdentityService>();

        services.AddSingleton<IPasswordHasher<Account>, PasswordHasher<Account>>();

        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<ModuleMarker>());

        return services;
    }
}

internal class ModuleMarker { }
