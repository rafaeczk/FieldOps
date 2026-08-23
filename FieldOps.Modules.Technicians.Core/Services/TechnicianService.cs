using FieldOps.Modules.Technicians.Core.DTOs;
using FieldOps.Modules.Technicians.Core.Events;
using FieldOps.Modules.Technicians.Core.Exceptions;
using FieldOps.Modules.Technicians.Core.Entities;
using FieldOps.Modules.Technicians.Core.Repositories;
using FieldOps.Modules.Technicians.Core.Services;
using FieldOps.Shared.Abstractions.Messaging;
using FieldOps.Shared.Abstractions.Time;
using Microsoft.Extensions.Hosting;

namespace FieldOps.Modules.Technicians.Core.Services;

internal class TechnicianService(IMessageClient moduleClient, ITechnicianRepository repository, IClock clock) : ITechnicianService
{
    private readonly IMessageClient moduleClient = moduleClient;
    private readonly ITechnicianRepository repository = repository;
    private readonly IClock clock = clock;

    public async Task<IReadOnlyList<TechnicianDto>> BrowseAsync()
    {
        var technicians = await repository.BrowseAsync();
        return technicians.Select(Map<TechnicianDto>).ToList();
    }

    public async Task<Guid> CreateAsync(CreateTechnicianDto dto)
    {
        var technician = Technician.Create(
           Guid.NewGuid(),
           dto.FullName,
           clock.UtcNow());

        await repository.CreateAsync(technician);

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
        {
            throw new TechnicianNotFoundException(id);
        }
        var accountId = technician.AccountId;
        await repository.DeleteAsync(technician);
        await moduleClient.PublishAsync(new TechnicianDeletedEvent(accountId));
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
