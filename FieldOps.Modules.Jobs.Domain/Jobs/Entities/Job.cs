using FieldOps.Modules.Jobs.Domain.Jobs.Events;
using FieldOps.Modules.Jobs.Domain.Jobs.Exceptions;
using FieldOps.Modules.Jobs.Domain.Jobs.ValueObjects;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Kernel.Types;

namespace FieldOps.Modules.Jobs.Domain.Jobs.Entities;

public sealed class Job : AggregateRoot
{
    public OperatorId CreatorId { get; private set; } = null!;
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public JobStatus Status { get; private set; } = null!;
    public JobPriority Priority { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    public DateTime Deadline { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Job() { }

    public static Job Create(AggregateId id, OperatorId creatorId, string title, 
        string? description, JobPriority priority, Address address, DateTime deadline, DateTime createdAt)
    {
        var job = new Job
        {
            Id = id,
            CreatorId = creatorId,
            Status = new(JobStatus.Pending),
            CreatedAt = createdAt,
            UpdatedAt = createdAt
        };

        job.ChangeTitle(title);
        job.ChangeDescription(description);
        job.ChangePriority(priority);
        job.ChangeAddress(address);
        job.ChangeDeadline(deadline);

        job.AddEvent(new JobAdded(job));

        return job;
    }

    // STATUSES
    public void SetInProgress()
    {
        if (Status != JobStatus.Pending)
            throw new InvalidJobStatusException(Id, JobStatus.InProgress);
        Status = new(JobStatus.InProgress);
        AddEvent(new JobStatusChanged(this, Status));
    }

    public void SetCompleted()
    {
        if (Status != JobStatus.InProgress)
            throw new InvalidJobStatusException(Id, JobStatus.Completed);
        Status = new(JobStatus.Completed);
        AddEvent(new JobStatusChanged(this, Status));
    }

    public void SetCancelled()
    {
        Status = new(JobStatus.Cancelled);
        AddEvent(new JobStatusChanged(this, Status));
    }

    // OTHER FIELDS
    public void ChangeTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new EmptyJobTitleException(Id);
        Title = title;
        IncrementVersion();
    }

    public void ChangeDescription(string? description)
    {
        Description = description;
        IncrementVersion();
    }

    public void ChangePriority(JobPriority priority)
    {
        Priority = priority;
        IncrementVersion();
    }

    public void ChangeAddress(Address address)
    {
        Address = address;
        IncrementVersion();
    }

    public void ChangeDeadline(DateTime deadline)
    {
        Deadline = deadline;
        IncrementVersion();
    }
}
