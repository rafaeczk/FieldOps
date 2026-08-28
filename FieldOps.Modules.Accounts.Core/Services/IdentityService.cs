using FieldOps.Modules.Accounts.Contracts.Events;
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
    IOutboxMessagesRepository outboxRepository,
    IAccountUnitOfWork unitOfWork,
    IPasswordHasher<Account> passwordHasher,
    IAuthManager authManager,
    IClock clock) : IIdentityService
{
    private readonly IAccountRepository accountRepository = accountRepository;
    private readonly IOutboxMessagesRepository outboxRepository = outboxRepository;
    private readonly IAccountUnitOfWork unitOfWork = unitOfWork;
    private readonly IPasswordHasher<Account> passwordHasher = passwordHasher;
    private readonly IAuthManager authManager = authManager;
    private readonly IClock clock = clock;

    public async Task<AccountDto?> GetAsync(Guid id)
    {
        var account = await accountRepository.GetAsync(id);

        if (account is null)
            return null;

        return new(account.Id, account.Email, account.FullName, account.Role, account.CreatedAt);
    }

    public async Task<JsonWebToken> SignInAsync(SignInCommand command)
    {
        var email = command.Email.ToLowerInvariant();
        var account = await accountRepository.GetAsync(email);

        if (account is null)
            throw new InvalidCredentialsException();

        if (passwordHasher.VerifyHashedPassword(default!, account.Hash, command.Password) == PasswordVerificationResult.Failed)
            throw new InvalidCredentialsException();

        var jwt = authManager.CreateToken(account.Id.ToString(), account.Role);
        jwt.Email = account.Email;
        jwt.FullName = account.FullName;
        jwt.CreatedAt = account.CreatedAt;

        return jwt;
    }

    public async Task CreateAccountAsync(CreateAccountCommand command)
    {
        var email = command.Email.ToLowerInvariant();

        var foundAccount = await accountRepository.GetAsync(email);

        if (foundAccount is not null)
            throw new EmailInUseException();

        var hash = passwordHasher.HashPassword(default!, command.Password);

        var account = Account.Create(command.Id, email, hash, command.FullName, command.Role, clock.UtcNow());

        await accountRepository.CreateAsync(account);
        await outboxRepository.CreateAsync(new AccountCreated(account.Id, account.Email, account.Role));

        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAccountAsync(Guid id)
    {
        var account = await accountRepository.GetAsync(id);

        if (account is null)
            throw new AccountNotFoundException();

        await accountRepository.DeleteAsync(account);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<AccountDto?> UpdateProfileAsync(Guid id, UpdateProfileCommand command)
    {
        var account = await accountRepository.GetAsync(id);

        if (account is null)
            throw new AccountNotFoundException();

        var email = command.Email.ToLowerInvariant();

        if (email != account.Email)
        {
            var existing = await accountRepository.GetAsync(email);
            if (existing is not null)
                throw new EmailInUseException();
        }

        account.UpdateProfile(email, command.FullName, clock.UtcNow());

        try
        {
            await unitOfWork.SaveChangesAsync();
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException)
        {
            throw new EmailInUseException();
        }

        return new AccountDto(account.Id, account.Email, account.FullName, account.Role, account.CreatedAt);
    }

    public async Task ChangePasswordAsync(Guid id, ChangePasswordCommand command)
    {
        if (command.NewPassword.Length < 6)
            throw new InvalidPasswordException("Password must be at least 6 characters");

        if (command.CurrentPassword == command.NewPassword)
            throw new InvalidPasswordException("New password must be different from current password");

        var account = await accountRepository.GetAsync(id);

        if (account is null)
            throw new AccountNotFoundException();

        if (passwordHasher.VerifyHashedPassword(default!, account.Hash, command.CurrentPassword) == PasswordVerificationResult.Failed)
            throw new InvalidCredentialsException();

        var hash = passwordHasher.HashPassword(default!, command.NewPassword);
        account.ChangePassword(hash, clock.UtcNow());

        try
        {
            await unitOfWork.SaveChangesAsync();
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException)
        {
            throw new InvalidPasswordException("Failed to update password");
        }
    }
}
