namespace FieldOps.Modules.Operators.Core.DTOs
{
    public record OperatorDto
    {
        public Guid Id { get; init; }
        public Guid AccountId { get; init; }
        public string FullName { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}