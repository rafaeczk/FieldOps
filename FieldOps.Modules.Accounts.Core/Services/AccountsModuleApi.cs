using FieldOps.Modules.Accounts.Contracts;
using FieldOps.Modules.Accounts.Core.Features.Queries;
using FieldOps.Shared.Abstractions.Messages;
using MediatR;

namespace FieldOps.Modules.Accounts.Core.Services;

internal class AccountsModuleApi(IMessageDispatcher messageDispatcher) : IAccountsModuleApi
{
    public async Task<bool> CheckAccountEmailIsTaken(string email)
    {
        return await messageDispatcher.Send(new CheckAccountEmailIsTakenQuery(email));
    }

    public async Task<string?> GetEmailByAccountId(Guid accountId)
    {
        return await messageDispatcher.Send(new GetEmailByAccountIdQuery(accountId));
    }
}
