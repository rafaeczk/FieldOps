using FieldOps.Modules.Accounts.Contracts;
using FieldOps.Modules.Operators.Contracts.Events;
using FieldOps.Modules.Operators.Core.DTOs;
using FieldOps.Modules.Operators.Core.Entities;
using FieldOps.Modules.Operators.Core.Exceptions;
using FieldOps.Modules.Operators.Core.Repositories;
using FieldOps.Modules.Operators.Core.Services;
using FieldOps.Shared.Abstractions.Time;
using MediatR;
using Moq;

namespace FieldOps.Modules.Operators.Tests;

public class OperatorServiceTests
{
    private readonly Mock<IOperatorRepository> _repositoryMock = new();
    private readonly Mock<IOutboxMessagesRepository> _outboxRepositoryMock = new();
    private readonly Mock<IOperatorUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IClock> _clockMock = new();
    private readonly Mock<IAccountsModuleApi> _accountsModuleApiMock = new();
    private readonly OperatorService _sut;

    public OperatorServiceTests()
    {
        _sut = new OperatorService(
            _repositoryMock.Object,
            _outboxRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _clockMock.Object,
            _accountsModuleApiMock.Object);
    }

    [Fact]
    public async Task CreateAsync_EmailNotTaken_CreatesOperatorAndPublishesEvent()
    {
        var dto = new CreateOperatorDto("John Doe", "john@test.com", "password123");
        var fixedTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        _clockMock.Setup(x => x.UtcNow()).Returns(fixedTime);
        _accountsModuleApiMock
            .Setup(x => x.CheckAccountEmailIsTaken(dto.RequestedEmail))
            .ReturnsAsync(false);

        var result = await _sut.CreateAsync(dto);

        Assert.NotEqual(Guid.Empty, result);
        _repositoryMock.Verify(x => x.CreateAsync(It.Is<Operator>(o =>
            o.FullName == dto.FullName)), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        _outboxRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<OperatorCreated>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_EmailTaken_ThrowsEmailInUseException()
    {
        var dto = new CreateOperatorDto("John Doe", "existing@test.com", "password123");

        _accountsModuleApiMock
            .Setup(x => x.CheckAccountEmailIsTaken(dto.RequestedEmail))
            .ReturnsAsync(true);

        await Assert.ThrowsAsync<EmailInUseException>(() => _sut.CreateAsync(dto));
    }

    [Fact]
    public async Task GetByAsync_ExistingOperator_ReturnsOperatorDto()
    {
        var operatorId = Guid.NewGuid();
        var @operator = Operator.Create(operatorId, "John Doe", DateTime.UtcNow);

        _repositoryMock
            .Setup(x => x.GetAsync(operatorId))
            .ReturnsAsync(@operator);

        var result = await _sut.GetByAsync(operatorId);

        Assert.NotNull(result);
        Assert.Equal(@operator.FullName, result.FullName);
        Assert.Equal(@operator.Id.Value, result.Id);
    }

    [Fact]
    public async Task GetByAsync_NonExistingOperator_ReturnsNull()
    {
        _repositoryMock
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Operator?)null);

        var result = await _sut.GetByAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task BrowseAsync_ReturnsListOfOperators()
    {
        var operators = new List<Operator>
        {
            Operator.Create(Guid.NewGuid(), "John Doe", DateTime.UtcNow),
            Operator.Create(Guid.NewGuid(), "Jane Doe", DateTime.UtcNow)
        };

        _repositoryMock
            .Setup(x => x.BrowseAsync())
            .ReturnsAsync(operators);

        var result = await _sut.BrowseAsync();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task DeleteAsync_ExistingOperator_DeletesOperatorAndPublishesEvent()
    {
        var operatorId = Guid.NewGuid();
        var @operator = Operator.Create(operatorId, "John Doe", DateTime.UtcNow);

        _repositoryMock
            .Setup(x => x.GetAsync(operatorId))
            .ReturnsAsync(@operator);

        await _sut.DeleteAsync(operatorId);

        _repositoryMock.Verify(x => x.DeleteAsync(@operator), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        _outboxRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<OperatorDeleted>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_NonExistingOperator_ThrowsOperatorNotFoundException()
    {
        _repositoryMock
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Operator?)null);

        await Assert.ThrowsAsync<OperatorNotFoundException>(() => _sut.DeleteAsync(Guid.NewGuid()));
    }
}
