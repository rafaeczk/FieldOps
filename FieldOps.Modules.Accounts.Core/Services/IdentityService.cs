using FieldOps.Modules.Accounts.Core.DTOs;
using FieldOps.Modules.Accounts.Core.Entities;
using FieldOps.Modules.Accounts.Core.Exceptions;
using FieldOps.Modules.Accounts.Core.Repositories;
using FieldOps.Shared.Abstractions.Auth;
using FieldOps.Shared.Abstractions.Time;
using Microsoft.AspNetCore.Identity;

namespace FieldOps.Modules.Accounts.Core.Services;

internal class IdentityService(
    IAccountRepository accountRepository, 
    IPasswordHasher<Account> passwordHasher, 
    IAuthManager authManager,
    IClock clock) : IIdentityService
{
    private readonly IAccountRepository accountRepository = accountRepository;
    private readonly IPasswordHasher<Account> passwordHasher = passwordHasher;
    private readonly IAuthManager authManager = authManager;
    private readonly IClock clock = clock;

    public async Task<AccountDto?> GetAsync(Guid id)
    {
        var account = await accountRepository.GetAsync(id);

        if (account is null)
            return null;

        return new(account.Email, account.Role, account.CreatedAt);
    }

    public async Task<JsonWebToken> SignInAsync(SignInDto dto)
    {
        var account = await accountRepository.GetAsync(dto.Email);

        if (account is null)
            throw new InvalidCredentialsException();

        if (passwordHasher.VerifyHashedPassword(default, account.Hash, dto.Password) == PasswordVerificationResult.Failed)
            throw new InvalidCredentialsException();

        var jwt = authManager.CreateToken(account.Id.ToString(), account.Role);

        return jwt;
    }

    public async Task SignUpAsync(SignUpDto dto)
    {
        var email = dto.Email.ToLowerInvariant();

        var foundAccount = await accountRepository.GetAsync(email);

        if (foundAccount is not null)
            throw new EmailInUseException();

        var hash = passwordHasher.HashPassword(default, dto.Password);

        var account = Account.Create(email, hash, "none", clock.UtcNow());

        await accountRepository.AddAsync(account);
    }
}
