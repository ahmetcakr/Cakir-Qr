using Core.Application.Responses;

namespace QrMenu.Application.Features.Auth.Commands.EnableOtpAuthenticator;

public class EnabledOtpAuthenticatorResponse : IResponse
{
    public string SecretKey { get; set; }

    public EnabledOtpAuthenticatorResponse()
    {
        SecretKey = string.Empty;
    }

    public EnabledOtpAuthenticatorResponse(string secretKey)
    {
        SecretKey = secretKey;
    }
}
