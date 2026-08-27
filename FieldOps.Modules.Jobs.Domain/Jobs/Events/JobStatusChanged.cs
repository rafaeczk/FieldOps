using FieldOps.Modules.Jobs.Domain.Jobs.Entities;
using FieldOps.Modules.Jobs.Domain.Jobs.ValueObjects;
using FieldOps.Shared.Abstractions.Kernel;

namespace FieldOps.Modules.Jobs.Domain.Jobs.Events;

public record JobStatusChanged(Job Job, JobStatus Status) : IDomainEvent;
