using System.Text.Json.Serialization;

namespace JWTAddons.Models;

public class GoogleResponse
{
    public GoogleResponseHeader Header { get; set; } = new GoogleResponseHeader();
    public GoogleResponsePayload Payload { get; set; } = new GoogleResponsePayload();
}
public class GoogleResponseHeader
{
    [JsonPropertyName("kid")]
    public string? Kid { get; set; }
    [JsonPropertyName("alg")]
    public string? Alg { get; set; }
    public string? Type { get; set; }
}
public class GoogleResponsePayload
{
    [JsonPropertyName("iss")]
    public string? Iss { get; set; }
    [JsonPropertyName("azp")]
    public string? Azp { get; set; }
    [JsonPropertyName("aud")]
    public string? Aud { get; set; }
    [JsonPropertyName("sub")]
    public string? Sub { get; set; }
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    [JsonPropertyName("email_verified")]
    public bool EmailVerified { get; set; }
    [JsonPropertyName("at_hash")]
    public string? AtHash { get; set; }
    [JsonPropertyName("nonce")]
    public string? Nonce { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("picture")]
    public string? Picture { get; set; }
    [JsonPropertyName("given_name")]
    public string? GivenName { get; set; }
    [JsonPropertyName("family_name")]
    public string? FamilyName { get; set; }
    [JsonPropertyName("locale")]
    public string? Locale { get; set; }
    [JsonPropertyName("iat")]
    public int Iat { get; set; }
    [JsonPropertyName("exp")]
    public int Exp { get; set; }
    public string? ExceptionMessage { get; set; }

}
