using FieldOps.Modules.Operators.Contracts;
using FieldOps.Modules.Operators.Core.Features.Queries;
using MediatR;

namespace FieldOps.Modules.Operators.Core.Services;

internal class OperatorsModuleApi(ISender sender) : IOperatorsModuleApi
{
    private readonly ISender sender = sender;

    public Task<Guid?> GetOperatorIdByAccountId(Guid accountId)
    {
        return sender.Send(new GetOperatorIdByAccountId(accountId));
    }
}
