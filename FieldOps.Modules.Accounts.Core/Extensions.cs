using FieldOps.Modules.Accounts.Core.DAL;
using FieldOps.Modules.Accounts.Core.DAL.Repositories;
using FieldOps.Modules.Accounts.Core.Entities;
using FieldOps.Modules.Accounts.Core.Repositories;
using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Shared.Infrastructure.Postgres;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Modules.Accounts.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<AccountsDbContext>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddSingleton<IPasswordHasher<Account>, PasswordHasher<Account>>();

        return services;
    }
}
