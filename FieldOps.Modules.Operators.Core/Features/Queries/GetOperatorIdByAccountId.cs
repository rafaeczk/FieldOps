using FieldOps.Modules.Operators.Core.Repositories;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Operators.Core.Features.Queries;

public record GetOperatorIdByAccountId(Guid AccountId) : IMessage<Guid?>;

internal class GetOperatorIdByAccountIdHandler(IOperatorRepository repository) : IMessageHandler<GetOperatorIdByAccountId, Guid?>
{
    public async Task<Guid?> HandleAsync(GetOperatorIdByAccountId request, CancellationToken ct)
    {
        var @operator = await repository.GetByAccountIdAsync(request.AccountId);
        return @operator?.Id;
    }
}
