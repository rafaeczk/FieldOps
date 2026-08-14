namespace FieldOps.Shared.Abstractions.Auth;

public class JsonWebToken
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public long Expires { get; set; }
    public string Id { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string Email { get; set; } = null!;
    public IDictionary<string, IEnumerable<string>> Claims { get; set; } = null!;
}
