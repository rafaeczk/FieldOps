using FieldOps.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Http;

namespace FieldOps.Shared.Infrastructure.Contexts;

public class Context : IContext
{
    public string RequestId { get; } = $"{Guid.NewGuid():N}";

    public string TraceId { get; } = null!;

    public IIdentityContext Identity { get; } = null!;

    internal Context()
    {
    }

    public Context(HttpContext context) : this(context.TraceIdentifier, new IdentityContext(context.User))
    {
    }

    internal Context(string traceId, IIdentityContext identity)
    {
        TraceId = traceId;
        Identity = identity;
    }

    public static IContext Empty => new Context();
}
