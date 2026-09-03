using FieldOps.Modules.Jobs.Application.Jobs.Services;
using FieldOps.Modules.Jobs.Domain.Jobs.Entities;
using FieldOps.Modules.Jobs.Domain.Jobs.ValueObjects;
using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Jobs.Tests;

public class EventMapperTests
{
    [Fact]
    public void Map_JobEvents_ToIntegrationEvents()
    {
        var mapper = new JobEventMapper();

        var creator = new OperatorId(Guid.NewGuid());
        var addr = new Address { CountryCode = "PL", PostalCode = "00-000", City = "W", Street = "S", BuildingNumber = "1" };
        var job = Job.Create(creator, "T", null, new JobPriority(JobPriority.Low), addr, DateTime.UtcNow, DateTime.UtcNow);

        var added = new Domain.Jobs.Events.JobAdded(job);
        var statusChanged = new Domain.Jobs.Events.JobStatusChanged(job, new JobStatus(JobStatus.InProgress));
        var assignee = JobAssignee.Create(new TechnicianId(Guid.NewGuid()), new(job.Id));
        var assigneeAdded = new Domain.Jobs.Events.JobAssigneeAdded(assignee);
        var assigneeRemoved = new Domain.Jobs.Events.JobAssigneeRemoved(assignee);

        var mapped = mapper.Map([added, statusChanged, assigneeAdded, assigneeRemoved]).ToList();

        Assert.Contains(mapped, e => e is Contracts.Events.JobAdded);
        Assert.Contains(mapped, e => e is Contracts.Events.JobStatusChanged);
        Assert.Contains(mapped, e => e is Contracts.Events.JobAssigneeAdded);
        Assert.Contains(mapped, e => e is Contracts.Events.JobAssigneeRemoved);
    }
}
