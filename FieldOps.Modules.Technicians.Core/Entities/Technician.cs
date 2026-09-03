using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Technicians.Core.Entities
{
    internal class Technician
    {
        public TechnicianId Id { get; private set; } = null!;
        public Guid AccountId { get; private set; }
        public string FullName { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }


        public static Technician Create(Guid accountId, string fullName, DateTime createdAt)
        {
            return new Technician
            {
                Id = Guid.NewGuid(),
                AccountId = accountId,
                FullName = fullName,
                CreatedAt = createdAt,
                UpdatedAt = createdAt
            };
        }
    }
}
