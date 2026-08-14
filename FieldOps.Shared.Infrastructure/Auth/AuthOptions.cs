namespace FieldOps.Shared.Infrastructure.Auth;

public class AuthOptions
{
    public bool AuthenticationDisabled { get; set; }
    public string Issuer { get; set; } = null!;
    public string IssuerSigningKey { get; set; } = null!;
    public string Authority { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string Challenge { get; set; } = "Bearer";
    public string MetadataAddress { get; set; } = null!;
    public bool SaveToken { get; set; } = true;
    public bool SaveSigninToken { get; set; }
    public bool RequireAudience { get; set; } = true;
    public bool RequireHttpsMetadata { get; set; } = true;
    public bool RequireExpirationTime { get; set; } = true;
    public bool RequireSignedTokens { get; set; } = true;
    public TimeSpan Expiry { get; set; }
    public string ValidAudience { get; set; } = null!;
    public IEnumerable<string> ValidAudiences { get; set; } = null!;
    public string ValidIssuer { get; set; } = null!;
    public IEnumerable<string> ValidIssuers { get; set; } = null!;
    public bool ValidateActor { get; set; }
    public bool ValidateAudience { get; set; } = true;
    public bool ValidateIssuer { get; set; } = true;
    public bool ValidateLifetime { get; set; } = true;
    public bool ValidateTokenReplay { get; set; }
    public bool ValidateIssuerSigningKey { get; set; }
    public bool RefreshOnIssuerKeyNotFound { get; set; } = true;
    public bool IncludeErrorDetails { get; set; } = true;
    public string AuthenticationType { get; set; } = null!;
    public string NameClaimType { get; set; } = null!;
    public string RoleClaimType { get; set; } = null!;
}
