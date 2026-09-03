using FieldOps.Modules.Accounts.Contracts;
using FieldOps.Modules.Operators.Contracts.Events;
using FieldOps.Modules.Operators.Core.DTOs;
using FieldOps.Modules.Operators.Core.Entities;
using FieldOps.Modules.Operators.Core.Exceptions;
using FieldOps.Modules.Operators.Core.Repositories;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Time;

namespace FieldOps.Modules.Operators.Core.Services;

internal class OperatorService(IOperatorRepository repository, IOutboxMessagesRepository outboxRepository, IOperatorUnitOfWork unitOfWork, 
    IClock clock, IAccountsModuleApi accountsModuleApi) : IOperatorService
{
    private readonly IOperatorRepository repository = repository;
    private readonly IOutboxMessagesRepository outboxRepository = outboxRepository;
    private readonly IOperatorUnitOfWork unitOfWork = unitOfWork;
    private readonly IClock clock = clock;
    private readonly IAccountsModuleApi accountsModuleApi = accountsModuleApi;

    public async Task<Guid> CreateAsync(CreateOperatorDto dto)
    {
        if (await accountsModuleApi.CheckAccountEmailIsTaken(dto.RequestedEmail))
            throw new EmailInUseException();

        var @operator = Operator.Create(
            Guid.NewGuid(),
            dto.FullName,
            clock.UtcNow());

        await repository.CreateAsync(@operator);

        await outboxRepository.CreateAsync(new OperatorCreated(
            @operator.Id,
            @operator.FullName,
            @operator.CreatedAt,
            @operator.AccountId,
            dto.RequestedEmail,
            dto.RequestedPassword));

        await unitOfWork.SaveChangesAsync();

        return @operator.Id;
    }

    public async Task<OperatorDetalisDto?> GetByAsync(OperatorId id)
    {
        var @operator = await repository.GetAsync(id);

        if (@operator is null)
            return null;

        var dto = Map<OperatorDetalisDto>(@operator);

        return dto;
    }

    public async Task<IReadOnlyList<OperatorDto>> BrowseAsync()
    {
        var operators = await repository.BrowseAsync();
        return [.. operators.Select(Map<OperatorDto>)];
    }

    public async Task DeleteAsync(OperatorId id)
    {
        var @operator = await repository.GetAsync(id);

        if (@operator is null)
            throw new OperatorNotFoundException(id);

        await repository.DeleteAsync(@operator);
        await outboxRepository.CreateAsync(new OperatorDeleted(@operator.AccountId));

        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteByAccountIdAsync(AccountId accountId)
    {
        var @operator = await repository.GetByAccountIdAsync(accountId);

        if (@operator is null)
            return;

        await repository.DeleteAsync(@operator);
        await outboxRepository.CreateAsync(new OperatorDeleted(@operator.AccountId));

        await unitOfWork.SaveChangesAsync();
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
