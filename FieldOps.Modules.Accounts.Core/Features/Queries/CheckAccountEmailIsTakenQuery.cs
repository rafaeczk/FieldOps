using FieldOps.Modules.Accounts.Core.Repositories;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Accounts.Core.Features.Queries;

public record CheckAccountEmailIsTakenQuery(string Email) : IMessage<bool>;

internal class CheckAccountEmailIsTakenQueryHandler(IAccountRepository repository) : IMessageHandler<CheckAccountEmailIsTakenQuery, bool>
{
    public async Task<bool> HandleAsync(CheckAccountEmailIsTakenQuery request, CancellationToken ct)
    {
        return await repository.GetAsync(request.Email) is not null;
    }
}
