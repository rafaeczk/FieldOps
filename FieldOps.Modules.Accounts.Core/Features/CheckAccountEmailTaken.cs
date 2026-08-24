using FieldOps.Modules.Accounts.Contracts;
using FieldOps.Modules.Accounts.Core.Repositories;
using MediatR;

namespace FieldOps.Modules.Accounts.Core.Features;

internal class CheckAccountEmailTaken(IAccountRepository repository) : IRequestHandler<CheckAccountEmailTakenQuery, bool>
{
    public async Task<bool> Handle(CheckAccountEmailTakenQuery request, CancellationToken ct)
    {
        return await repository.GetAsync(request.Email) is not null;
    }
}
