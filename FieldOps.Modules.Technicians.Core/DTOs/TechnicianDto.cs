namespace FieldOps.Modules.Technicians.Core.DTOs
{
    public record TechnicianDto
    {
        public Guid Id { get; init; }
        public Guid AccountId { get; init; }
        public string FullName { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}