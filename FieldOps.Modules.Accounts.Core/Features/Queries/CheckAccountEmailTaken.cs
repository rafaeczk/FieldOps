using FieldOps.Modules.Accounts.Contracts.Queries;
using FieldOps.Modules.Accounts.Core.Repositories;
using MediatR;

namespace FieldOps.Modules.Accounts.Core.Features.Queries;

internal class CheckAccountEmailTaken(IAccountRepository repository) : IRequestHandler<Contracts.Queries.CheckAccountEmailIsTaken, bool>
{
    public async Task<bool> Handle(Contracts.Queries.CheckAccountEmailIsTaken request, CancellationToken ct)
    {
        return await repository.GetAsync(request.Email) is not null;
    }
}
