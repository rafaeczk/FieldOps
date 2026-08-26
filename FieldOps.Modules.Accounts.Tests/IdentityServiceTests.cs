using FieldOps.Modules.Accounts.Core.DTOs;
using FieldOps.Modules.Accounts.Core.Entities;
using FieldOps.Modules.Accounts.Core.Exceptions;
using FieldOps.Modules.Accounts.Core.Events;
using FieldOps.Modules.Accounts.Core.Repositories;
using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Modules.Accounts.Core.ValueObjects;
using FieldOps.Shared.Abstractions.Auth;
using FieldOps.Shared.Abstractions.Messaging;
using FieldOps.Shared.Abstractions.Time;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace FieldOps.Modules.Accounts.Tests;

public class IdentityServiceTests
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock = new();
    private readonly Mock<IPasswordHasher<Account>> _passwordHasherMock = new();
    private readonly Mock<IAuthManager> _authManagerMock = new();
    private readonly Mock<IMessageClient> _messageClientMock = new();
    private readonly Mock<IClock> _clockMock = new();
    private readonly IdentityService _sut;

    public IdentityServiceTests()
    {
        _sut = new IdentityService(
            _accountRepositoryMock.Object,
            _passwordHasherMock.Object,
            _authManagerMock.Object,
            _messageClientMock.Object,
            _clockMock.Object);
    }

    [Fact]
    public async Task SignInAsync_ValidCredentials_ReturnsJwt()
    {
        var email = "test@test.com";
        var password = "password123";
        var account = Account.Create(email, "hash", new AccountRole(AccountRole.Admin), DateTime.UtcNow);

        _accountRepositoryMock
            .Setup(x => x.GetAsync(email))
            .ReturnsAsync(account);

        _passwordHasherMock
            .Setup(x => x.VerifyHashedPassword(default!, account.Hash, password))
            .Returns(PasswordVerificationResult.Success);

        _authManagerMock
            .Setup(x => x.CreateToken(account.Id.ToString(), account.Role, It.IsAny<string>(), It.IsAny<IDictionary<string, IEnumerable<string>>>()))
            .Returns(new JsonWebToken { AccessToken = "jwt-token", Expires = 1234567890 });

        var result = await _sut.SignInAsync(new SignInCommand(email, password));

        Assert.NotNull(result);
        Assert.Equal("jwt-token", result.AccessToken);
    }

    [Fact]
    public async Task SignInAsync_InvalidEmail_ThrowsInvalidCredentialsException()
    {
        _accountRepositoryMock
            .Setup(x => x.GetAsync("nonexistent@test.com"))
            .ReturnsAsync((Account?)null);

        await Assert.ThrowsAsync<InvalidCredentialsException>(
            () => _sut.SignInAsync(new SignInCommand("nonexistent@test.com", "password")));
    }

    [Fact]
    public async Task SignInAsync_InvalidPassword_ThrowsInvalidCredentialsException()
    {
        var email = "test@test.com";
        var account = Account.Create(email, "hash", new AccountRole(AccountRole.Admin), DateTime.UtcNow);

        _accountRepositoryMock
            .Setup(x => x.GetAsync(email))
            .ReturnsAsync(account);

        _passwordHasherMock
            .Setup(x => x.VerifyHashedPassword(default!, account.Hash, "wrongpassword"))
            .Returns(PasswordVerificationResult.Failed);

        await Assert.ThrowsAsync<InvalidCredentialsException>(
            () => _sut.SignInAsync(new SignInCommand(email, "wrongpassword")));
    }

    [Fact]
    public async Task CreateAccountAsync_ValidCommand_CreatesAccountAndPublishesEvent()
    {
        var command = new CreateAccountCommand(Guid.NewGuid(), "test@test.com", "password123", new AccountRole(AccountRole.Operator));
        var fixedTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        _clockMock.Setup(x => x.UtcNow()).Returns(fixedTime);
        _accountRepositoryMock
            .Setup(x => x.GetAsync(command.Email))
            .ReturnsAsync((Account?)null);
        _passwordHasherMock
            .Setup(x => x.HashPassword(default!, command.Password))
            .Returns("hashed-password");

        await _sut.CreateAccountAsync(command);

        _accountRepositoryMock.Verify(x => x.AddAsync(It.Is<Account>(a =>
            a.Email == command.Email &&
            a.Hash == "hashed-password" &&
            a.Role == command.Role)), Times.Once);

        _messageClientMock.Verify(x => x.PublishAsync(It.IsAny<AccountCreatedEvent>()), Times.Once);
    }

    [Fact]
    public async Task CreateAccountAsync_DuplicateEmail_ThrowsEmailInUseException()
    {
        var command = new CreateAccountCommand(Guid.NewGuid(), "existing@test.com", "password123", new AccountRole(AccountRole.Operator));
        var existingAccount = Account.Create("existing@test.com", "hash", new AccountRole(AccountRole.Operator), DateTime.UtcNow);

        _accountRepositoryMock
            .Setup(x => x.GetAsync(command.Email))
            .ReturnsAsync(existingAccount);

        await Assert.ThrowsAsync<EmailInUseException>(
            () => _sut.CreateAccountAsync(command));
    }

    [Fact]
    public async Task DeleteAccountAsync_ExistingAccount_DeletesAccount()
    {
        var accountId = Guid.NewGuid();
        var account = Account.Create(accountId, "test@test.com", "hash", new AccountRole(AccountRole.Admin), DateTime.UtcNow);

        _accountRepositoryMock
            .Setup(x => x.GetAsync(accountId))
            .ReturnsAsync(account);

        await _sut.DeleteAccountAsync(accountId);

        _accountRepositoryMock.Verify(x => x.DeleteAsync(account), Times.Once);
    }

    [Fact]
    public async Task DeleteAccountAsync_NonExistingAccount_ThrowsAccountNotFoundException()
    {
        _accountRepositoryMock
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Account?)null);

        await Assert.ThrowsAsync<AccountNotFoundException>(
            () => _sut.DeleteAccountAsync(Guid.NewGuid()));
    }

    [Fact]
    public async Task GetAsync_ExistingAccount_ReturnsAccountDto()
    {
        var accountId = Guid.NewGuid();
        var account = Account.Create(accountId, "test@test.com", "hash", new AccountRole(AccountRole.Admin), DateTime.UtcNow);

        _accountRepositoryMock
            .Setup(x => x.GetAsync(accountId))
            .ReturnsAsync(account);

        var result = await _sut.GetAsync(accountId);

        Assert.NotNull(result);
        Assert.Equal(account.Email, result.Email);
        Assert.Equal((string)account.Role, result.Role);
    }

    [Fact]
    public async Task GetAsync_NonExistingAccount_ReturnsNull()
    {
        _accountRepositoryMock
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Account?)null);

        var result = await _sut.GetAsync(Guid.NewGuid());

        Assert.Null(result);
    }
}
