using FieldOps.Modules.Accounts.Core.Repositories;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Accounts.Core.Features.Queries;

public record CheckAccountEmailIsTaken(string Email) : IMessage<bool>;

internal class CheckAccountEmailIsTakenHandler(IAccountRepository repository) : IMessageHandler<CheckAccountEmailIsTaken, bool>
{
    public async Task<bool> HandleAsync(CheckAccountEmailIsTaken request, CancellationToken ct)
    {
        return await repository.GetAsync(request.Email) is not null;
    }
}
