using FieldOps.Modules.WorkOrders.Core.DTOs;
using FieldOps.Modules.WorkOrders.Core.Entities;
using FieldOps.Modules.WorkOrders.Core.Events;
using FieldOps.Modules.WorkOrders.Core.Exceptions;
using FieldOps.Modules.WorkOrders.Core.Repositories;
using FieldOps.Modules.WorkOrders.Core.Services;
using FieldOps.Modules.WorkOrders.Core.ValueObjects;
using FieldOps.Shared.Abstractions.Messaging;
using FieldOps.Shared.Abstractions.Time;
using Moq;

namespace FieldOps.Modules.WorkOrders.Tests;

public class WorkOrderServiceTests
{
    private readonly Mock<IWorkOrderRepository> _repositoryMock = new();
    private readonly Mock<IWorkOrderUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IMessageClient> _messageClientMock = new();
    private readonly Mock<IClock> _clockMock = new();
    private readonly WorkOrderService _sut;

    public WorkOrderServiceTests()
    {
        _sut = new WorkOrderService(
            _repositoryMock.Object,
            _unitOfWorkMock.Object,
            _messageClientMock.Object,
            _clockMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ValidCommand_CreatesWorkOrderAndPublishesEvent()
    {
        var dto = new CreateWorkOrderDto("Fix plumbing", "Leaking pipe", "123 Main St", DateTime.UtcNow.AddDays(1));
        var operatorId = Guid.NewGuid();
        var fixedTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        _clockMock.Setup(x => x.UtcNow()).Returns(fixedTime);

        var result = await _sut.CreateAsync(dto, operatorId);

        Assert.NotEqual(Guid.Empty, result);
        _repositoryMock.Verify(x => x.CreateAsync(It.Is<WorkOrder>(wo =>
            wo.Title == dto.Title &&
            wo.Address == dto.Address &&
            wo.OperatorId == operatorId)), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        _messageClientMock.Verify(x => x.PublishAsync(It.IsAny<WorkOrderCreatedEvent>()), Times.Once);
    }

    [Fact]
    public async Task GetByAsync_ExistingWorkOrder_ReturnsWorkOrderDto()
    {
        var workOrderId = Guid.NewGuid();
        var workOrder = WorkOrder.Create(
            "Fix plumbing", "Leaking pipe", "123 Main St",
            DateTime.UtcNow.AddDays(1), Guid.NewGuid(), DateTime.UtcNow);

        _repositoryMock
            .Setup(x => x.GetAsync(workOrderId))
            .ReturnsAsync(workOrder);

        var result = await _sut.GetByAsync(workOrderId);

        Assert.NotNull(result);
        Assert.Equal(workOrder.Title, result.Title);
        Assert.Equal(workOrder.Id, result.Id);
    }

    [Fact]
    public async Task GetByAsync_NonExistingWorkOrder_ReturnsNull()
    {
        _repositoryMock
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((WorkOrder?)null);

        var result = await _sut.GetByAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task BrowseByOperatorAsync_ReturnsListOfWorkOrders()
    {
        var operatorId = Guid.NewGuid();
        var workOrders = new List<WorkOrder>
        {
            WorkOrder.Create("Fix plumbing", null, "123 Main St", DateTime.UtcNow.AddDays(1), operatorId, DateTime.UtcNow),
            WorkOrder.Create("Fix electrical", null, "456 Oak Ave", DateTime.UtcNow.AddDays(2), operatorId, DateTime.UtcNow)
        };

        _repositoryMock
            .Setup(x => x.BrowseByOperatorAsync(operatorId))
            .ReturnsAsync(workOrders);

        var result = await _sut.BrowseByOperatorAsync(operatorId);

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task BrowseByTechnicianAsync_ReturnsListOfWorkOrders()
    {
        var technicianId = Guid.NewGuid();
        var workOrders = new List<WorkOrder>
        {
            WorkOrder.Create("Fix plumbing", null, "123 Main St", DateTime.UtcNow.AddDays(1), Guid.NewGuid(), DateTime.UtcNow),
            WorkOrder.Create("Fix electrical", null, "456 Oak Ave", DateTime.UtcNow.AddDays(2), Guid.NewGuid(), DateTime.UtcNow)
        };

        _repositoryMock
            .Setup(x => x.BrowseByTechnicianAsync(technicianId))
            .ReturnsAsync(workOrders);

        var result = await _sut.BrowseByTechnicianAsync(technicianId);

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task BrowseAllAsync_ReturnsListOfWorkOrders()
    {
        var workOrders = new List<WorkOrder>
        {
            WorkOrder.Create("Fix plumbing", null, "123 Main St", DateTime.UtcNow.AddDays(1), Guid.NewGuid(), DateTime.UtcNow),
            WorkOrder.Create("Fix electrical", null, "456 Oak Ave", DateTime.UtcNow.AddDays(2), Guid.NewGuid(), DateTime.UtcNow)
        };

        _repositoryMock
            .Setup(x => x.BrowseAllAsync())
            .ReturnsAsync(workOrders);

        var result = await _sut.BrowseAllAsync();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task UpdateStatusAsync_ExistingWorkOrder_UpdatesStatusAndPublishesEvent()
    {
        var workOrderId = Guid.NewGuid();
        var workOrder = WorkOrder.Create(
            "Fix plumbing", "Leaking pipe", "123 Main St",
            DateTime.UtcNow.AddDays(1), Guid.NewGuid(), DateTime.UtcNow);

        _repositoryMock
            .Setup(x => x.GetAsync(workOrderId))
            .ReturnsAsync(workOrder);

        var dto = new UpdateWorkOrderStatusDto(WorkOrderStatus.InProgress);
        await _sut.UpdateStatusAsync(workOrderId, dto);

        Assert.Equal(WorkOrderStatus.InProgress, workOrder.Status);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        _messageClientMock.Verify(x => x.PublishAsync(It.IsAny<WorkOrderStatusChangedEvent>()), Times.Once);
    }

    [Fact]
    public async Task UpdateStatusAsync_NonExistingWorkOrder_ThrowsWorkOrderNotFoundException()
    {
        _repositoryMock
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((WorkOrder?)null);

        await Assert.ThrowsAsync<WorkOrderNotFoundException>(
            () => _sut.UpdateStatusAsync(Guid.NewGuid(), new UpdateWorkOrderStatusDto(WorkOrderStatus.Completed)));
    }

    [Fact]
    public async Task UpdateStatusAsync_InvalidStatus_ThrowsInvalidWorkOrderStatusException()
    {
        var workOrderId = Guid.NewGuid();
        var workOrder = WorkOrder.Create(
            "Fix plumbing", "Leaking pipe", "123 Main St",
            DateTime.UtcNow.AddDays(1), Guid.NewGuid(), DateTime.UtcNow);

        _repositoryMock
            .Setup(x => x.GetAsync(workOrderId))
            .ReturnsAsync(workOrder);

        await Assert.ThrowsAsync<InvalidWorkOrderStatusException>(
            () => _sut.UpdateStatusAsync(workOrderId, new UpdateWorkOrderStatusDto("INVALID")));
    }

    [Fact]
    public async Task AssignTechnicianAsync_ExistingWorkOrder_AssignsAndPublishesEvent()
    {
        var workOrderId = Guid.NewGuid();
        var technicianId = Guid.NewGuid();
        var workOrder = WorkOrder.Create(
            "Fix plumbing", "Leaking pipe", "123 Main St",
            DateTime.UtcNow.AddDays(1), Guid.NewGuid(), DateTime.UtcNow);

        _repositoryMock
            .Setup(x => x.GetAsync(workOrderId))
            .ReturnsAsync(workOrder);

        var dto = new AssignTechnicianDto(technicianId);
        await _sut.AssignTechnicianAsync(workOrderId, dto);

        Assert.Equal(technicianId, workOrder.TechnicianId);
        Assert.Equal(WorkOrderStatus.InProgress, workOrder.Status);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        _messageClientMock.Verify(x => x.PublishAsync(It.IsAny<WorkOrderStatusChangedEvent>()), Times.Once);
    }

    [Fact]
    public async Task AssignTechnicianAsync_AlreadyAssigned_ThrowsWorkOrderAlreadyAssignedException()
    {
        var workOrderId = Guid.NewGuid();
        var workOrder = WorkOrder.Create(
            "Fix plumbing", "Leaking pipe", "123 Main St",
            DateTime.UtcNow.AddDays(1), Guid.NewGuid(), DateTime.UtcNow);
        workOrder.AssignTechnician(Guid.NewGuid(), DateTime.UtcNow);

        _repositoryMock
            .Setup(x => x.GetAsync(workOrderId))
            .ReturnsAsync(workOrder);

        await Assert.ThrowsAsync<WorkOrderAlreadyAssignedException>(
            () => _sut.AssignTechnicianAsync(workOrderId, new AssignTechnicianDto(Guid.NewGuid())));
    }

    [Fact]
    public async Task AssignTechnicianAsync_NonExistingWorkOrder_ThrowsWorkOrderNotFoundException()
    {
        _repositoryMock
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((WorkOrder?)null);

        await Assert.ThrowsAsync<WorkOrderNotFoundException>(
            () => _sut.AssignTechnicianAsync(Guid.NewGuid(), new AssignTechnicianDto(Guid.NewGuid())));
    }

    [Fact]
    public async Task DeleteAsync_ExistingWorkOrder_DeletesAndPublishesNothing()
    {
        var workOrderId = Guid.NewGuid();
        var workOrder = WorkOrder.Create(
            "Fix plumbing", "Leaking pipe", "123 Main St",
            DateTime.UtcNow.AddDays(1), Guid.NewGuid(), DateTime.UtcNow);

        _repositoryMock
            .Setup(x => x.GetAsync(workOrderId))
            .ReturnsAsync(workOrder);

        await _sut.DeleteAsync(workOrderId);

        _repositoryMock.Verify(x => x.DeleteAsync(workOrder), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_NonExistingWorkOrder_ThrowsWorkOrderNotFoundException()
    {
        _repositoryMock
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((WorkOrder?)null);

        await Assert.ThrowsAsync<WorkOrderNotFoundException>(
            () => _sut.DeleteAsync(Guid.NewGuid()));
    }
}
