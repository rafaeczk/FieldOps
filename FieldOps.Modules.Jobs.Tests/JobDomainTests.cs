using FieldOps.Modules.Jobs.Domain.Jobs.Entities;
using FieldOps.Modules.Jobs.Domain.Jobs.Exceptions;
using FieldOps.Modules.Jobs.Domain.Jobs.Events;
using FieldOps.Modules.Jobs.Domain.Jobs.ValueObjects;
using FieldOps.Shared.Abstractions.Errors;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Kernel.ValueObjects;

namespace FieldOps.Modules.Jobs.Tests;

public class JobDomainTests
{
    [Fact]
    public void Create_SetsVersionAndAddsJobAddedEvent()
    {
        var creator = new OperatorId(Guid.NewGuid());
        var addr = new Address { CountryCode = "PL", PostalCode = "00-000", City = "Warsaw", Street = "Main", BuildingNumber = "1" };
        var now = DateTime.UtcNow;

        var job = Job.Create(creator, "Title", "Desc", new JobPriority(JobPriority.Medium), addr, now.AddDays(1), now);

        Assert.Equal(1, job.Version);
        Assert.Contains(job.Events, e => e is JobAdded);
    }

    [Fact]
    public void AddAssignee_AddsAssigneeAndEmitsEvent()
    {
        var creator = new OperatorId(Guid.NewGuid());
        var addr = new Address { CountryCode = "PL", PostalCode = "00-000", City = "Warsaw", Street = "Main", BuildingNumber = "1" };
        var job = Job.Create(creator, "Title", null, new JobPriority(JobPriority.Low), addr, DateTime.UtcNow, DateTime.UtcNow);
        var tech = new TechnicianId(Guid.NewGuid());

        job.AddAssignee(tech);

        Assert.Single(job.Assignees);
        Assert.Contains(job.Events, e => e is JobAssigneeAdded);
    }

    [Fact]
    public void RemoveAssignee_RemovesAndEmitsEvent()
    {
        var creator = new OperatorId(Guid.NewGuid());
        var addr = new Address { CountryCode = "PL", PostalCode = "00-000", City = "Warsaw", Street = "Main", BuildingNumber = "1" };
        var job = Job.Create(creator, "Title", null, new JobPriority(JobPriority.Low), addr, DateTime.UtcNow, DateTime.UtcNow);
        var tech = new TechnicianId(Guid.NewGuid());

        job.AddAssignee(tech);
        // clear events recorded so far to make assertion specific to remove
        // AggregateRoot does not expose ClearEvents, so recreate job state by using existing assignee removal

        job.RemoveAssignee(tech);

        Assert.Empty(job.Assignees);
        Assert.Contains(job.Events, e => e is JobAssigneeRemoved);
    }

    [Fact]
    public void SetInProgress_InvalidTransition_Throws()
    {
        var creator = new OperatorId(Guid.NewGuid());
        var addr = new Address { CountryCode = "PL", PostalCode = "00-000", City = "Warsaw", Street = "Main", BuildingNumber = "1" };
        var job = Job.Create(creator, "Title", null, new(JobPriority.Low), addr, DateTime.UtcNow, DateTime.UtcNow);

        // move to InProgress first time - valid
        job.SetInProgress();

        // calling SetInProgress again should throw because status is not Pending
        Assert.Throws<InvalidJobStatusException>(() => job.SetInProgress());
    }

    [Fact]
    public void JobPriority_Invalid_ThrowsInvalidJobPriorityException()
    {
        Assert.Throws<InvalidJobPriorityException>(() => new JobPriority("UNKNOWN"));
    }

    [Fact]
    public void JobStatus_Invalid_ThrowsInvalidJobStatusException()
    {
        Assert.Throws<InvalidJobStatusException>(() => new JobStatus("UNKNOWN"));
    }
}
