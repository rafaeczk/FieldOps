using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Technicians.Core.Entities
{
    internal class Technician
    {
        public Guid Id { get; private set; }
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
