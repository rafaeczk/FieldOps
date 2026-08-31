namespace FieldOps.Shared.Abstractions.Kernel;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
