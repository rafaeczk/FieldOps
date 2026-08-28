using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Modules.Accounts.Core.ValueObjects;
using FieldOps.Modules.Operators.Contracts.Events;
using MediatR;

namespace FieldOps.Modules.Accounts.Core.Features.Events;

internal class OperatorCreatedHandler(IIdentityService identity) : INotificationHandler<OperatorCreated>
{
    public async Task Handle(OperatorCreated @event, CancellationToken ct)
    {
        await identity.CreateAccountAsync(new(@event.RequestedAccountId, @event.RequestedEmail, @event.RequestedPassword, @event.FullName, new(AccountRole.Operator)));
    }
}
