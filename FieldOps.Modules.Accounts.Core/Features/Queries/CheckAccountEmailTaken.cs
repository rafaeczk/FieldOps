using FieldOps.Modules.Accounts.Core.Repositories;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Accounts.Core.Features.Queries;

internal class CheckAccountEmailTaken(IAccountRepository repository) : IMessageHandler<Contracts.Queries.CheckAccountEmailIsTaken, bool>
{
    public async Task<bool> HandleAsync(Contracts.Queries.CheckAccountEmailIsTaken request, CancellationToken ct)
    {
        return await repository.GetAsync(request.Email) is not null;
    }
}
