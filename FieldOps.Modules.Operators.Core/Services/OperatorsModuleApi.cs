using FieldOps.Modules.Operators.Contracts;
using FieldOps.Modules.Operators.Core.Features.Queries;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using MediatR;

namespace FieldOps.Modules.Operators.Core.Services;

internal class OperatorsModuleApi(ISender sender) : IOperatorsModuleApi
{
    private readonly ISender sender = sender;

    public Task<OperatorId?> GetOperatorIdByAccountId(Guid accountId)
    {
        return sender.Send(new GetOperatorIdByAccountIdQuery(accountId));
    }
}
