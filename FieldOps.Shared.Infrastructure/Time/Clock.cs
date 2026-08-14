using FieldOps.Shared.Abstractions.Time;

namespace FieldOps.Shared.Infrastructure.Time;

public class Clock : IClock
{
    public DateTime UtcNow() => DateTime.UtcNow;
}
