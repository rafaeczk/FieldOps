namespace FieldOps.Modules.Accounts.Core.Entities;

internal class Account
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = null!;
    public string Hash { get; private set; } = null!;
    public string Role { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }

    private Account() { }

    public static Account Create(string email, string hash, string role, DateTime createdAt)
    {
        return new Account
        {
            Id = Guid.NewGuid(),
            Email = email,
            Hash = hash,
            Role = role,
            CreatedAt = createdAt
        };
    }
}
