using FieldOps.Modules.Operators.Core.Repositories;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Operators.Core.Features.Queries;

public record GetOperatorIdByAccountIdQuery(Guid AccountId) : IMessage<OperatorId?>;

internal class GetOperatorIdByAccountIdQueryHandler(IOperatorRepository repository) : IMessageHandler<GetOperatorIdByAccountIdQuery, OperatorId?>
{
    public async Task<OperatorId?> HandleAsync(GetOperatorIdByAccountIdQuery request, CancellationToken ct)
    {
        var @operator = await repository.GetByAccountIdAsync(request.AccountId);
        return @operator?.Id;
    }
}
