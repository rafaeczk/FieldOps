using FieldOps.Modules.Accounts.Contracts;
using FieldOps.Modules.Operators.Core.DTOs;
using FieldOps.Modules.Operators.Core.Entities;
using FieldOps.Modules.Operators.Core.Events;
using FieldOps.Modules.Operators.Core.Exceptions;
using FieldOps.Modules.Operators.Core.Repositories;
using FieldOps.Shared.Abstractions.Messaging;
using FieldOps.Shared.Abstractions.Time;
using MediatR;

namespace FieldOps.Modules.Operators.Core.Services;

internal class OperatorService(IOperatorRepository repository, IOperatorUnitOfWork unitOfWork, IMessageClient messageClient, IClock clock, ISender sender) : IOperatorService
{
    private readonly IOperatorRepository repository = repository;
    private readonly IOperatorUnitOfWork unitOfWork = unitOfWork;
    private readonly IMessageClient messageClient = messageClient;
    private readonly IClock clock = clock;
    private readonly ISender sender = sender;

    public async Task<Guid> CreateAsync(CreateOperatorDto dto)
    {
        if (await sender.Send(new CheckAccountEmailTakenQuery(dto.RequestedEmail)))
            throw new EmailInUseException();

        var @operator = Operator.Create(
            Guid.NewGuid(),
            dto.FullName,
            clock.UtcNow());

        await repository.CreateAsync(@operator);

        await unitOfWork.SaveChangesAsync();

        await messageClient.PublishAsync(new OperatorCreatedEvent(
            @operator.Id,
            @operator.FullName,
            @operator.CreatedAt,
            @operator.AccountId,
            dto.RequestedEmail,
            dto.RequestedPassword));

        return @operator.Id;
    }

    public async Task<OperatorDetalisDto?> GetByAsync(Guid id)
    {
        var @operator = await repository.GetAsync(id);

        if (@operator is null)
            return null;

        var dto = Map<OperatorDetalisDto>(@operator);
       
        return dto;
    }

    public async Task<IReadOnlyList<OperatorDto>> BrowseAsync()
    {
        var operators =  await repository.BrowseAsync();
        return [.. operators.Select(Map<OperatorDto>)];
    }

    public async Task DeleteAsync(Guid id)
    {
        var @operator = await repository.GetAsync(id);

        if (@operator is null)
            throw new OperatorNotFoundException(id);

        await repository.DeleteAsync(@operator);
        await unitOfWork.SaveChangesAsync();

        await messageClient.PublishAsync(new OperatorDeletedEvent(@operator.AccountId));
    }

    private static T Map<T>(Operator @operator) where T : OperatorDto, new()
         => new()
         {
             Id = @operator.Id,
             AccountId = @operator.AccountId,
             FullName = @operator.FullName,
             CreatedAt = @operator.CreatedAt,
             UpdatedAt = @operator.UpdatedAt
         };
}
