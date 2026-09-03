using FieldOps.Shared.Abstractions.Kernel.Types;

namespace FieldOps.Modules.Accounts.Core.ValueObjects;

public class AccountRole : ValueObject
{
    public string Value { get; }

    public const string Admin = "ADMIN";
    public const string Operator = "OPERATOR";
    public const string Technician = "TECHNICIAN";
    public static readonly HashSet<string> AcceptedValues = [Admin, Operator, Technician];

    public AccountRole(string value)
    {
        if (!AcceptedValues.Contains(value))
            throw new ArgumentException("The provided value is not a valid role.");

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(AccountRole role) => role.Value;

    public static explicit operator AccountRole(string value) => new(value);
}
