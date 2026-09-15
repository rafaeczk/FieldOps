using FieldOps.Modules.Jobs.Domain.Jobs.Entities;
using FieldOps.Shared.Abstractions.Kernel;

namespace FieldOps.Modules.Jobs.Domain.Jobs.Events;

public record JobAdded(Job Job) : IDomainEvent;
