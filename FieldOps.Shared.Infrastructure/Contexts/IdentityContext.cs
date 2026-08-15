using FieldOps.Shared.Abstractions.Contexts;
using System.Security.Claims;

namespace FieldOps.Shared.Infrastructure.Contexts;

internal class IdentityContext : IIdentityContext
{
    public bool IsAuthenticated { get; }
    public Guid Id { get; }
    public string Role { get; } = null!;
    public Dictionary<string, IEnumerable<string>> Claims { get; } = [];

    public IdentityContext(ClaimsPrincipal principal)
    {
        IsAuthenticated = principal.Identity?.IsAuthenticated is true;
        Id = IsAuthenticated ? Guid.Parse(principal.Identity!.Name!) : Guid.Empty;
        Role = principal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value ?? null!;
        Claims = principal.Claims.GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.Select(c => c.Value.ToString()));
    }
}
