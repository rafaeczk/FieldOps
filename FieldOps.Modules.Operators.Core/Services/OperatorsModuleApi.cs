using FieldOps.Modules.Operators.Contracts;
using FieldOps.Modules.Operators.Core.Features.Queries;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Operators.Core.Services;

internal class OperatorsModuleApi(IMessageDispatcher messageDispatcher) : IOperatorsModuleApi
{
    private readonly IMessageDispatcher messageDispatcher = messageDispatcher;

    public Task<OperatorId?> GetOperatorIdByAccountId(AccountId accountId)
    {
        return messageDispatcher.Send(new GetOperatorIdByAccountIdQuery(accountId));
    }
}
