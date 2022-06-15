using System.Text.Json.Serialization;

namespace JWTAddons.Models;

public class AppleResponse
{
    public AppleResponseHeader Header { get; set; } = new AppleResponseHeader();
    public AppleResponsePayload Payload { get; set; } = new AppleResponsePayload();
}

public class AppleResponseHeader
{
    [JsonPropertyName("kid")]
    public string? Kid { get; set; }
    [JsonPropertyName("alg")]
    public string? Alg { get; set; }
}
public class AppleResponsePayload
{
    [JsonPropertyName("iss")]
    public string? Iss { get; set; }
    [JsonPropertyName("aud")]
    public string? Aud { get; set; }
    [JsonPropertyName("exp")]
    public int Exp { get; set; }
    [JsonPropertyName("iat")]
    public int Iat { get; set; }
    [JsonPropertyName("sub")]
    public string? Sub { get; set; }
    [JsonPropertyName("c_hash")]
    public string? CHash { get; set; }
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    [JsonPropertyName("email_verified")]
    public string? EmailVerified { get; set; }
    [JsonPropertyName("auth_time")]
    public int AuthTime { get; set; }
    [JsonPropertyName("nonce_supported")]
    public bool NonceSupported { get; set; }
    public string? ExceptionMessage { get; set; }
}
