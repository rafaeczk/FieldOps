using FieldOps.Shared.Abstractions.Kernel.Types;

namespace FieldOps.Shared.Abstractions.Kernel.ValueObjects;

public class Address : ValueObject
{
    public required string CountryCode { get; init; }
    public required string PostalCode { get; init; }
    public required string City { get; init; }
    public required string Street { get; init; }
    public required string BuildingNumber { get; init; }
    public string? ApartmentNumber { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CountryCode;
        yield return PostalCode;
        yield return City;
        yield return Street;
        yield return BuildingNumber;
        yield return ApartmentNumber!;
    }
}
