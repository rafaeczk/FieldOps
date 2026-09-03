using FieldOps.Modules.Accounts.Contracts;
using FieldOps.Modules.Accounts.Core.Features.Queries;
using MediatR;

namespace FieldOps.Modules.Accounts.Core.Services;

internal class AccountsModuleApi(ISender sender) : IAccountsModuleApi
{
    public async Task<bool> CheckAccountEmailIsTaken(string email)
    {
        return await sender.Send(new CheckAccountEmailIsTakenQuery(email));
    }
}
