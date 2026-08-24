using FieldOps.Modules.Operators.Core.DTOs;
using FieldOps.Modules.Operators.Core.Events;
using FieldOps.Modules.Operators.Core.Repositories;
using FieldOps.Shared.Abstractions.Messaging;

namespace FieldOps.Modules.Operators.Core.Services;

internal class OperatorService(IMessageClient messageClient, IOperatorRepository repository) : IOperatorService
{
    private readonly IMessageClient messageClient = messageClient;
    private readonly IOperatorRepository repository = repository;

    public async Task<Guid> CreateAsync(CreateOperatorDto dto)
    {
        var @operator = await repository.CreateAsync(dto);

        await messageClient.PublishAsync(new OperatorCreatedEvent(
            @operator.Id,
            @operator.FullName,
            @operator.CreatedAt,
            @operator.AccountId,
            dto.RequestedEmail,
            dto.RequestedPassword));

        return @operator.Id;
    }
}
