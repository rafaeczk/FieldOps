using FieldOps.Modules.Accounts.Core.Entities;
using FieldOps.Modules.Accounts.Core.ValueObjects;
using FieldOps.Shared.Abstractions.Time;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FieldOps.Modules.Accounts.Core.DAL;

internal class AccountInitializer(
    IServiceScopeFactory scopeFactory,
    IWebHostEnvironment env,
    ILogger<AccountInitializer> logger,
    IPasswordHasher<Account> hasher,
    IClock clock) : IHostedService
{
    private readonly IServiceScopeFactory scopeFactory = scopeFactory;
    private readonly IWebHostEnvironment env = env;
    private readonly ILogger<AccountInitializer> logger = logger;
    private readonly IPasswordHasher<Account> hasher = hasher;
    private readonly IClock clock = clock;

    public async Task StartAsync(CancellationToken ct)
    {
        if (!env.IsDevelopment())
            return;

        using var scope = scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AccountDbContext>();

        var adminRole = new AccountRole(AccountRole.Admin);

        if (await dbContext.Accounts.AnyAsync(a => a.Role == adminRole, ct))
        {
            logger.LogInformation("The admin already exists");
            return;
        }

        var email = "admin@fieldops.com";
        var password = "123";

        var admin = Account.Create(
            email,
            hasher.HashPassword(default!, password),
            adminRole,
            clock.UtcNow());

        dbContext.Accounts.Add(admin);
        await dbContext.SaveChangesAsync(ct);

        logger.LogInformation("The admin has been created {email} {password}", email, password);
    }

    public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
}
