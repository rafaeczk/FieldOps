using FieldOps.Modules.Accounts.Contracts;
using FieldOps.Modules.Technicians.Core.DTOs;
using FieldOps.Modules.Technicians.Core.Entities;
using FieldOps.Modules.Technicians.Core.Events;
using FieldOps.Modules.Technicians.Core.Exceptions;
using FieldOps.Modules.Technicians.Core.Repositories;
using FieldOps.Shared.Abstractions.Messaging;
using FieldOps.Shared.Abstractions.Time;
using MediatR;

namespace FieldOps.Modules.Technicians.Core.Services;

internal class TechnicianService(ITechnicianRepository repository, ITechnicianUnitOfWork unitOfWork, IMessageClient moduleClient, IClock clock, ISender sender) : ITechnicianService
{
    private readonly ITechnicianRepository repository = repository;
    private readonly ITechnicianUnitOfWork unitOfWork = unitOfWork;
    private readonly IMessageClient moduleClient = moduleClient;
    private readonly IClock clock = clock;
    private readonly ISender sender = sender;

    public async Task<IReadOnlyList<TechnicianDto>> BrowseAsync()
    {
        var technicians = await repository.BrowseAsync();
        return [.. technicians.Select(Map<TechnicianDto>)];
    }

    public async Task<Guid> CreateAsync(CreateTechnicianDto dto)
    {
        if (await sender.Send(new CheckAccountEmailTakenQuery(dto.RequestedEmail)))
            throw new EmailInUseException();

        var technician = Technician.Create(
           Guid.NewGuid(),
           dto.FullName,
           clock.UtcNow());

        await repository.CreateAsync(technician);
        await unitOfWork.SaveChangesAsync();

        await moduleClient.PublishAsync(new TechnicianCreatedEvent(
            technician.Id,
            technician.FullName,
            technician.CreatedAt,
            technician.AccountId,
            dto.RequestedEmail,
            dto.RequestedPassword));

        return technician.Id;
    }

    public async Task DeleteAsync(Guid id)
    {
        var technician = await repository.GetAsync(id);

        if (technician is null)
            throw new TechnicianNotFoundException(id);

        await repository.DeleteAsync(technician);
        await unitOfWork.SaveChangesAsync();

        await moduleClient.PublishAsync(new TechnicianDeletedEvent(technician.AccountId));
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
