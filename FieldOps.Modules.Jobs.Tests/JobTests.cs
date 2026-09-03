using FieldOps.Modules.Jobs.Domain.Jobs.Entities;
using FieldOps.Modules.Jobs.Domain.Jobs.Exceptions;
using FieldOps.Modules.Jobs.Domain.Jobs.ValueObjects;
using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Jobs.Tests;

public class JobTests
{
    [Fact]
    public void Create_WithValidData_CreatesJobAndAddsEvent()
    {
        var creator = new OperatorId(Guid.NewGuid());
        var addr = new Address { CountryCode = "PL", PostalCode = "00-000", City = "Warsaw", Street = "Main", BuildingNumber = "1" };
        var job = Job.Create(creator, "Title", "Desc", new(JobPriority.Medium), addr, DateTime.UtcNow, DateTime.UtcNow);

        Assert.NotNull(job.Id);
        Assert.Equal("Title", job.Title);
        Assert.Equal(creator, job.CreatorId);
        Assert.NotEmpty(job.Events);
    }

    [Fact]
    public void Create_WithEmptyTitle_Throws()
    {
        var creator = new OperatorId(Guid.NewGuid());
        var addr = new Address { CountryCode = "PL", PostalCode = "00-000", City = "Warsaw", Street = "Main", BuildingNumber = "1" };

        Assert.Throws<EmptyJobTitleException>(() => Job.Create(creator, " ", null, new JobPriority(JobPriority.Low), addr, DateTime.UtcNow, DateTime.UtcNow));
    }

    [Fact]
    public void AddAssignee_Duplicate_Throws()
    {
        var creator = new OperatorId(Guid.NewGuid());
        var addr = new Address { CountryCode = "PL", PostalCode = "00-000", City = "Warsaw", Street = "Main", BuildingNumber = "1" };
        var job = Job.Create(creator, "Title", null, new JobPriority(JobPriority.Low), addr, DateTime.UtcNow, DateTime.UtcNow);
        var tech = new TechnicianId(Guid.NewGuid());

        job.AddAssignee(tech);

        Assert.Throws<AssigneeAlreadyExists>(() => job.AddAssignee(tech));
    }

    [Fact]
    public void RemoveAssignee_NotExists_Throws()
    {
        var creator = new OperatorId(Guid.NewGuid());
        var addr = new Address { CountryCode = "PL", PostalCode = "00-000", City = "Warsaw", Street = "Main", BuildingNumber = "1" };
        var job = Job.Create(creator, "Title", null, new JobPriority(JobPriority.Low), addr, DateTime.UtcNow, DateTime.UtcNow);

        Assert.Throws<AssigneeDoesntExist>(() => job.RemoveAssignee(new TechnicianId(Guid.NewGuid())));
    }

    [Fact]
    public void StatusTransitions_WorkAsExpected()
    {
        var creator = new OperatorId(Guid.NewGuid());
        var addr = new Address { CountryCode = "PL", PostalCode = "00-000", City = "Warsaw", Street = "Main", BuildingNumber = "1" };
        var job = Job.Create(creator, "Title", null, new JobPriority(JobPriority.Low), addr, DateTime.UtcNow, DateTime.UtcNow);

        job.SetInProgress();
        Assert.Equal(JobStatus.InProgress, job.Status.Value);

        job.SetCompleted();
        Assert.Equal(JobStatus.Completed, job.Status.Value);
    }

    [Fact]
    public void UnauthorizedEdit_Throws()
    {
        var creator = new OperatorId(Guid.NewGuid());
        var addr = new Address { CountryCode = "PL", PostalCode = "00-000", City = "Warsaw", Street = "Main", BuildingNumber = "1" };
        var job = Job.Create(creator, "Title", null, new JobPriority(JobPriority.Low), addr, DateTime.UtcNow, DateTime.UtcNow);

        Assert.Throws<UnauthorizedJobAccessException>(() => job.EnsureCanBeEdited(new OperatorId(Guid.NewGuid())));
    }
}
