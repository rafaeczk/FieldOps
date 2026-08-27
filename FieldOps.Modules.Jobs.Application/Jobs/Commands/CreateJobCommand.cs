using FieldOps.Modules.Jobs.Domain.Jobs.Entities;
using FieldOps.Modules.Jobs.Domain.Jobs.Repositories;
using FieldOps.Modules.Jobs.Domain.Jobs.ValueObjects;
using FieldOps.Shared.Abstractions.Contexts;
using FieldOps.Shared.Abstractions.Kernel.Types;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Jobs.Application.Jobs.Commands;

public record CreateJobCommand(string Title, string? Description, JobPriority Priority, Address Address, DateTime Deadline) : IMessage;

public sealed class CreateJobCommandHandler(IJobRepository repository, IContext context) : IMessageHandler<CreateJobCommand>
{
    public async Task HandleAsync(CreateJobCommand message, CancellationToken ct)
    {
        //var job = Job.Create(
        //    new AggregateId(),
        //    context.Identity.Id,
        //    message.Title,
        //    message.Description,
        //    message.Priority
        //    );

        //repository.Add(job);
    }
}
