using FieldOps.Modules.Accounts.Core.DTOs;
using FieldOps.Modules.Accounts.Core.ValueObjects;
using FieldOps.Shared.Abstractions.Auth;

namespace FieldOps.Modules.Accounts.Core.Services;

public interface IIdentityService
{
    Task<AccountDto?> GetAsync(Guid id);
    Task<JsonWebToken> SignInAsync(SignInCommand dto);
    Task CreateAccountAsync(CreateAccountCommand dto);
    Task DeleteAccountAsync(Guid id);

}

public record SignInCommand(string Email, string Password);

public record CreateAccountCommand(Guid Id, string Email, string Password, AccountRole Role);
