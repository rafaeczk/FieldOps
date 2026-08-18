using FieldOps.Modules.Accounts.Core.ValueObjects;

namespace FieldOps.Modules.Accounts.Core.Entities;

internal class Account
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = null!;
    public string Hash { get; private set; } = null!;
    public AccountRole Role { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Account() { }
    public static Account Create(string email, string hash, AccountRole role, DateTime createdAt)
    {
        return new Account
        {
            Id = Guid.NewGuid(),
            Email = email,
            Hash = hash,
            Role = role,
            CreatedAt = createdAt,
            UpdatedAt = createdAt
        };
    }

    public static Account Create(Guid id, string email, string hash, AccountRole role, DateTime createdAt)
    {
        return new Account
        {
            Id = id,
            Email = email,
            Hash = hash,
            Role = role,
            CreatedAt = createdAt,
            UpdatedAt = createdAt
        };
    }
}
