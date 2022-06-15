using JWTAddons.Models;

namespace JWTAddons;

public interface ISocialLoginValidators
{
    Task<AppleResponse> ValidateAppleTokenAsync(string accessToken);
    Task<FacebookResponse> ValidateFacebookTokenAsync(string accessToken);
    Task<GoogleResponse> ValidateGoogleTokenAsync(string accessToken);
}