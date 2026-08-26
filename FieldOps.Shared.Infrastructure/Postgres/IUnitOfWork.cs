namespace FieldOps.Shared.Infrastructure.Postgres;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
