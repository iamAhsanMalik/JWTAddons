

using System.Text.Json.Serialization;

namespace JWTAddons.Models;

public class FacebookResponse
{
    public ResponseData Data { get; set; } = new ResponseData();
    public string? ExceptionMessage { get; set; }
}
public class ResponseData
{
    [JsonPropertyName("app_id")]
    public string? AppId { get; set; }
    public string? Type { get; set; }
    public string? Application { get; set; }
    [JsonPropertyName("data_access_expires_at")]
    public int DataAccessExpiresAt { get; set; }
    [JsonPropertyName("expires_at")]
    public int ExpiresAt { get; set; }
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    [JsonPropertyName("is_valid")]
    public bool IsValid { get; set; }
    [JsonPropertyName("issued_at")]
    public int IssuedAt { get; set; }
    public Metadata Metadata { get; set; } = new Metadata();
    public string[]? Scopes { get; set; }
    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }
}
public class Metadata
{
    [JsonPropertyName("auth_type")]
    public string? AuthType { get; set; }
    public string? Sso { get; set; }
}
