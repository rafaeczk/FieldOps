using FieldOps.Modules.Accounts.Core.Repositories;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Accounts.Core.Features.Queries;

public record GetEmailByAccountIdQuery(Guid AccountId) : IMessage<string?>;

internal class GetEmailByAccountIdQueryHandler(IAccountRepository repository) : IMessageHandler<GetEmailByAccountIdQuery, string?>
{
    public async Task<string?> HandleAsync(GetEmailByAccountIdQuery request, CancellationToken ct)
    {
        var account = await repository.GetAsync(new AccountId(request.AccountId));
        return account?.Email;
    }
}
