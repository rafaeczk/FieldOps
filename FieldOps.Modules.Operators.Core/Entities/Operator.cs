using FieldOps.Shared.Abstractions.Time;

namespace FieldOps.Modules.Operators.Core.Entities;

internal class Operator
{
    public Guid Id { get; private set; }
    public Guid AccountId { get; private set; }
    public string FullName { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Operator() { }

    public static Operator Create(Guid accountId, string fullName, DateTime createdAt)
    {
        return new Operator
        {
            Id = Guid.NewGuid(),
            AccountId = accountId,
            FullName = fullName,
            CreatedAt = createdAt,
            UpdatedAt = createdAt
        };
    }

    public void UpdateDetails(string fullName, IClock clock)
    {
        FullName = fullName;
        UpdatedAt = clock.UtcNow();
    }
}
