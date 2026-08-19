    using FieldOps.Modules.Operators.Core.DTOs;
using FieldOps.Modules.Operators.Core.Entities;
using FieldOps.Modules.Operators.Core.Events;
using FieldOps.Modules.Operators.Core.Exceptions;
using FieldOps.Modules.Operators.Core.Repositories;
using FieldOps.Shared.Abstractions.Messaging;
using FieldOps.Shared.Abstractions.Time;
using Microsoft.Extensions.Hosting;

namespace FieldOps.Modules.Operators.Core.Services;

internal class OperatorService(IMessageClient moduleClient, IOperatorRepository repository, IClock clock) : IOperatorService
{
    private readonly IMessageClient moduleClient = moduleClient;
    private readonly IOperatorRepository repository = repository;
    private readonly IClock clock = clock;

    public async Task<Guid> CreateAsync(CreateOperatorDto dto)
    {
        var @operator = Operator.Create(
            Guid.NewGuid(),
            dto.FullName,
            clock.UtcNow());

        await repository.CreateAsync(@operator);

        await moduleClient.PublishAsync(new OperatorCreatedEvent(
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
        {
            return null;
        }

        var dto = Map<OperatorDetalisDto>(@operator);
       
        return dto;
    }

 

    public async Task<IReadOnlyList<OperatorDto>> BrowseAsync()
    {
        var operators =  await repository.BrowseAsync();
        return operators.Select(Map<OperatorDto>).ToList();
    }

    public async Task DeleteAsync(Guid id)
    {
        var @operator = await repository.GetAsync(id);

        if (@operator is null)
        {
            throw new OperatorNotFoundException(id);
        }
        var accountId = @operator.AccountId;
        await repository.DeleteAsync(@operator);
        await moduleClient.PublishAsync(new OperatorDeletedEvent(accountId));
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
