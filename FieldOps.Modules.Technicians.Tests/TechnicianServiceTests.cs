using FieldOps.Modules.Accounts.Contracts;
using FieldOps.Modules.Technicians.Core.DTOs;
using FieldOps.Modules.Technicians.Core.Entities;
using FieldOps.Modules.Technicians.Core.Events;
using FieldOps.Modules.Technicians.Core.Exceptions;
using FieldOps.Modules.Technicians.Core.Repositories;
using FieldOps.Modules.Technicians.Core.Services;
using FieldOps.Shared.Abstractions.Messaging;
using FieldOps.Shared.Abstractions.Time;
using MediatR;
using Moq;

namespace FieldOps.Modules.Technicians.Tests;

public class TechnicianServiceTests
{
    private readonly Mock<ITechnicianRepository> _repositoryMock = new();
    private readonly Mock<ITechnicianUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IMessageClient> _messageClientMock = new();
    private readonly Mock<IClock> _clockMock = new();
    private readonly Mock<ISender> _senderMock = new();
    private readonly TechnicianService _sut;

    public TechnicianServiceTests()
    {
        _sut = new TechnicianService(
            _repositoryMock.Object,
            _unitOfWorkMock.Object,
            _messageClientMock.Object,
            _clockMock.Object,
            _senderMock.Object);
    }

    [Fact]
    public async Task CreateAsync_EmailNotTaken_CreatesTechnicianAndPublishesEvent()
    {
        var dto = new CreateTechnicianDto("John Smith", "john@test.com", "password123");
        var fixedTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        _clockMock.Setup(x => x.UtcNow()).Returns(fixedTime);
        _senderMock
            .Setup(x => x.Send(It.IsAny<CheckAccountEmailTakenQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var result = await _sut.CreateAsync(dto);

        Assert.NotEqual(Guid.Empty, result);
        _repositoryMock.Verify(x => x.CreateAsync(It.Is<Technician>(t =>
            t.FullName == dto.FullName)), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        _messageClientMock.Verify(x => x.PublishAsync(It.IsAny<TechnicianCreatedEvent>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_EmailTaken_ThrowsEmailInUseException()
    {
        var dto = new CreateTechnicianDto("John Smith", "existing@test.com", "password123");

        _senderMock
            .Setup(x => x.Send(It.IsAny<CheckAccountEmailTakenQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        await Assert.ThrowsAsync<EmailInUseException>(() => _sut.CreateAsync(dto));
    }

    [Fact]
    public async Task GetByAsync_ExistingTechnician_ReturnsTechnicianDto()
    {
        var technicianId = Guid.NewGuid();
        var technician = Technician.Create(technicianId, "John Smith", DateTime.UtcNow);

        _repositoryMock
            .Setup(x => x.GetAsync(technicianId))
            .ReturnsAsync(technician);

        var result = await _sut.GetByAsync(technicianId);

        Assert.NotNull(result);
        Assert.Equal(technician.FullName, result.FullName);
        Assert.Equal(technician.Id, result.Id);
    }

    [Fact]
    public async Task GetByAsync_NonExistingTechnician_ReturnsNull()
    {
        _repositoryMock
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Technician?)null);

        var result = await _sut.GetByAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task BrowseAsync_ReturnsListOfTechnicians()
    {
        var technicians = new List<Technician>
        {
            Technician.Create(Guid.NewGuid(), "John Smith", DateTime.UtcNow),
            Technician.Create(Guid.NewGuid(), "Jane Smith", DateTime.UtcNow)
        };

        _repositoryMock
            .Setup(x => x.BrowseAsync())
            .ReturnsAsync(technicians);

        var result = await _sut.BrowseAsync();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task DeleteAsync_ExistingTechnician_DeletesTechnicianAndPublishesEvent()
    {
        var technicianId = Guid.NewGuid();
        var technician = Technician.Create(technicianId, "John Smith", DateTime.UtcNow);

        _repositoryMock
            .Setup(x => x.GetAsync(technicianId))
            .ReturnsAsync(technician);

        await _sut.DeleteAsync(technicianId);

        _repositoryMock.Verify(x => x.DeleteAsync(technician), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        _messageClientMock.Verify(x => x.PublishAsync(It.IsAny<TechnicianDeletedEvent>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_NonExistingTechnician_ThrowsTechnicianNotFoundException()
    {
        _repositoryMock
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Technician?)null);

        await Assert.ThrowsAsync<TechnicianNotFoundException>(() => _sut.DeleteAsync(Guid.NewGuid()));
    }
}
