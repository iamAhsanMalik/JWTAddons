using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text.Json;
using JWTAddons.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JWTAddons;

public class SocialLoginValidators : ISocialLoginValidators
{
    private readonly ProvidersConfigurationOptions _options;
    private readonly IHttpClientFactory _httpClient;

    public SocialLoginValidators(IOptions<ProvidersConfigurationOptions> options, IHttpClientFactory httpClient)
    {
        _options = options.Value;
        _httpClient = httpClient;
    }

    public async Task<AppleResponse> ValidateAppleTokenAsync(string accessToken)
    {
        #region Parameters Declaration

        HttpClient client = _httpClient.CreateClient();
        string kid = string.Empty;

        TokenValidationParameters validationParameters = new TokenValidationParameters();
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        AppleResponse appleResponse = new AppleResponse();
        #endregion


        var response = await client.GetAsync(_options.Apple.PublicKey);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("Something went wrong with Apple Public Key");
        }

        try
        {
            var applePublicKeys = await JsonSerializer.DeserializeAsync<AppleKeys>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            kid = new JwtSecurityTokenHandler().ReadJwtToken(accessToken).Header.Kid;

            JsonWebKey? publicKey = applePublicKeys?.Keys?.FirstOrDefault(key => key.Kid == _options.Apple.KID);
            if (publicKey == null)
            {
                throw new Exception($"kid {kid} not found in apple public keys");
            }

            validationParameters = new TokenValidationParameters();

            tokenHandler = new JwtSecurityTokenHandler();

            validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = publicKey,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = _options.Apple.ISS,
                ValidAudience = _options.Apple.AUD,
            };


            tokenHandler.ValidateToken(accessToken, validationParameters, out var validatedToken);
            var appleResponseHeader = JsonSerializer.Deserialize<AppleResponseHeader>(tokenHandler.ReadJwtToken(accessToken).Header.SerializeToJson());

            appleResponse.Header = appleResponseHeader ?? new AppleResponseHeader();
            var appleResponsePayload = JsonSerializer.Deserialize<AppleResponsePayload>(tokenHandler.ReadJwtToken(accessToken).Payload.SerializeToJson());
            appleResponse.Payload = appleResponsePayload ?? new AppleResponsePayload();
            return appleResponse;
        }
        catch (Exception ex)
        {
            appleResponse.Payload.ExceptionMessage = $"Apple Token is not valid {ex.Message}";
            return appleResponse;
        }
    }

    public async Task<GoogleResponse> ValidateGoogleTokenAsync(string accessToken)
    {

        #region Parameters Declaration
        var client = new HttpClient();
        TokenValidationParameters validationParameters = new TokenValidationParameters();
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        GoogleResponse googleResponse = new GoogleResponse();
        string kid = string.Empty;
        #endregion

        var response = await client.GetAsync(_options.Google.PublicKey);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("Something went wrong with Google Public Key");
        }

        try
        {
            var googlePublicKeys = await JsonSerializer.DeserializeAsync<GoogleKeys>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            kid = new JwtSecurityTokenHandler().ReadJwtToken(accessToken).Header.Kid;

            JsonWebKey? publicKey = googlePublicKeys?.Keys?.FirstOrDefault(key => key.Kid == _options.Google.KID);
            if (publicKey == null)
            {
                throw new Exception($"kid {kid} not found in Google public keys");
            }

            validationParameters = new TokenValidationParameters();

            tokenHandler = new JwtSecurityTokenHandler();

            validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = publicKey,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = _options.Google.ISS,
                ValidAudience = _options.Google.AUD,
            };


            tokenHandler.ValidateToken(accessToken, validationParameters, out var validatedToken);

            var googleResponseHeader = JsonSerializer.Deserialize<GoogleResponseHeader>(tokenHandler.ReadJwtToken(accessToken).Header.SerializeToJson());
            var googleResponsePayload = JsonSerializer.Deserialize<GoogleResponsePayload>(tokenHandler.ReadJwtToken(accessToken).Payload.SerializeToJson());
            googleResponse.Header = googleResponseHeader ?? new GoogleResponseHeader();
            googleResponse.Payload = googleResponsePayload ?? new GoogleResponsePayload();
            return googleResponse;
        }
        catch (Exception ex)
        {
            googleResponse.Payload.ExceptionMessage = $"Google Token is not valid {ex.Message}";
            return googleResponse;
        }
    }

    public async Task<FacebookResponse> ValidateFacebookTokenAsync(string accessToken)
    {
        string? facebookClientId = _options.Facebook.ClientId;
        string? facebookClientSecret = _options.Facebook.ClientSecret;

        FacebookResponse requestResult = new FacebookResponse();
        var facebookUrl = $"https://graph.facebook.com/debug_token?input_token={accessToken}&access_token={facebookClientId}|{facebookClientSecret}";

        try
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(facebookUrl);
                //return await _httpClient.CreateClient().GetFromJsonAsync<FacebookResponse>(facebookUrl);

                if (result.IsSuccessStatusCode)
                {
                    FacebookResponse? facebookResponse = JsonSerializer.Deserialize<FacebookResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    requestResult = facebookResponse ?? new FacebookResponse();

                    if (requestResult.Data.IsValid)
                    {
                        return requestResult;
                    }
                    else
                    {
                        requestResult.ExceptionMessage = $"Access Token in Invalid or Expired";
                        return requestResult;
                    }
                }
                else
                {
                    requestResult.ExceptionMessage = $"{result.IsSuccessStatusCode}";
                    return requestResult;
                }
            };

        }
        catch (Exception ex)
        {
            requestResult.ExceptionMessage = $"Facebook Token is not valid {ex.Message}";
            return requestResult;
        }
    }
}
#region Apple Keys
public class AppleKeys
{
    public List<JsonWebKey>? Keys { get; set; }
}
#endregion

#region Google Keys
public class GoogleKeys
{
    public List<JsonWebKey>? Keys { get; set; }
}
#endregion

