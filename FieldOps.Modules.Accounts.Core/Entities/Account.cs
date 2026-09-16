using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Kernel.ValueObjects;

namespace FieldOps.Modules.Accounts.Core.Entities;

internal class Account
{
    public AccountId Id { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string FullName { get; private set; } = null!;
    public string Hash { get; private set; } = null!;
    public AccountRole Role { get; private set; } = null!;
    public bool MustChangePassword { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Account() { }
    public static Account Create(string email, string fullName, string hash, AccountRole role, DateTime createdAt)
    {
        return new Account
        {
            Id = Guid.NewGuid(),
            Email = email,
            FullName = fullName,
            Hash = hash,
            Role = role,
            MustChangePassword = true,
            CreatedAt = createdAt,
            UpdatedAt = createdAt
        };
    }

    public static Account Create(Guid id, string email, string fullName, string hash, AccountRole role, DateTime createdAt)
    {
        return new Account
        {
            Id = id,
            Email = email,
            FullName = fullName,
            Hash = hash,
            Role = role,
            MustChangePassword = true,
            CreatedAt = createdAt,
            UpdatedAt = createdAt
        };
    }

    public void UpdateProfile(string email, string fullName, DateTime updatedAt)
    {
        Email = email;
        FullName = fullName;
        UpdatedAt = updatedAt;
    }

    public void ChangePassword(string hash, DateTime updatedAt)
    {
        Hash = hash;
        MustChangePassword = false;
        UpdatedAt = updatedAt;
    }

    internal void AcknowledgePasswordChange()
    {
        MustChangePassword = false;
    }
}
