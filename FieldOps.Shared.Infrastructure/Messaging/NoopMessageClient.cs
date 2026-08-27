using FieldOps.Shared.Abstractions.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace FieldOps.Shared.Infrastructure.Messaging;

internal class NoopMessageClient : IMessageClient
{
    public Task PublishAsync<T>(T @event, CancellationToken ct = default) where T : class
    {
        return Task.CompletedTask;
    }
}
