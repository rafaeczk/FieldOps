using FieldOps.Modules.Accounts.Core.DTOs;
using FieldOps.Modules.Accounts.Core.ValueObjects;
using FieldOps.Shared.Abstractions.Auth;
using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Accounts.Core.Services;

public interface IIdentityService
{
    Task<AccountDto?> GetAsync(AccountId id);
    Task<JsonWebToken> SignInAsync(SignInCommand dto);
    Task CreateAccountAsync(CreateAccountCommand dto);
    Task DeleteAccountAsync(AccountId id);

}

public record SignInCommand(string Email, string Password);

public record CreateAccountCommand(AccountId Id, string Email, string Password, AccountRole Role);
