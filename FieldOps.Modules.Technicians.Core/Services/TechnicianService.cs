using FieldOps.Modules.Accounts.Contracts;
using FieldOps.Modules.Technicians.Contracts.Events;
using FieldOps.Modules.Technicians.Core.DTOs;
using FieldOps.Modules.Technicians.Core.Entities;
using FieldOps.Modules.Technicians.Core.Exceptions;
using FieldOps.Modules.Technicians.Core.Repositories;
using FieldOps.Shared.Abstractions.Time;

namespace FieldOps.Modules.Technicians.Core.Services;

internal class TechnicianService(ITechnicianRepository repository, IOutboxMessagesRepository outboxRepository, ITechnicianUnitOfWork unitOfWork, 
    IClock clock, IAccountsModuleApi accountsModuleApi) : ITechnicianService
{
    private readonly ITechnicianRepository repository = repository;
    private readonly IOutboxMessagesRepository outboxRepository = outboxRepository;
    private readonly ITechnicianUnitOfWork unitOfWork = unitOfWork;
    private readonly IClock clock = clock;
    private readonly IAccountsModuleApi accountsModuleApi = accountsModuleApi;

    public async Task<IReadOnlyList<TechnicianDto>> BrowseAsync()
    {
        var technicians = await repository.BrowseAsync();
        return [.. technicians.Select(Map<TechnicianDto>)];
    }

    public async Task<Guid> CreateAsync(CreateTechnicianDto dto)
    {
        if (await accountsModuleApi.CheckAccountEmailIsTaken(dto.RequestedEmail))
            throw new EmailInUseException();

        var technician = Technician.Create(
           Guid.NewGuid(),
           dto.FullName,
           clock.UtcNow());

        await repository.CreateAsync(technician);

        await outboxRepository.CreateAsync(new TechnicianCreated(
            technician.Id,
            technician.FullName,
            technician.CreatedAt,
            technician.AccountId,
            dto.RequestedEmail,
            dto.RequestedPassword));

        await unitOfWork.SaveChangesAsync();

        return technician.Id;
    }

    public async Task DeleteAsync(Guid id)
    {
        var technician = await repository.GetAsync(id);

        if (technician is null)
            throw new TechnicianNotFoundException(id);

        await repository.DeleteAsync(technician);

        await outboxRepository.CreateAsync(new TechnicianDeleted(technician.AccountId));

        await unitOfWork.SaveChangesAsync();
    }

    public async Task<TechnicianDto?> GetByAsync(Guid id)
    {
        var technician = await repository.GetAsync(id);
        if (technician is null)
        {
            return null;
        }

        var dto = Map<TechnicianDto>(technician);

        return dto;
    }

    private static T Map<T>(Technician technician) where T : TechnicianDto, new()
        => new()
        {
            Id = technician.Id,
            AccountId = technician.AccountId,
            FullName = technician.FullName,
            CreatedAt = technician.CreatedAt,
            UpdatedAt = technician.UpdatedAt
        };
}
