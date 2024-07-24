using Core.Security.Entities;

namespace QrMenu.Application.Services.AuthenticatorService;

public interface IAuthenticatorService
{
    public Task<EmailAuthenticator> CreateEmailAuthenticator(User user);
    public Task<EmailAuthenticator> VerifyEmailAuthenticator(EmailAuthenticator emailAuthenticator);
    public Task<OtpAuthenticator> CreateOtpAuthenticator(User user);
    public Task<string> ConvertSecretKeyToString(byte[] secretKey);
    public Task SendAuthenticatorCode(User user);
    public Task SendAuthenticatorCode(int userId);
    public Task VerifyAuthenticatorCode(User user, string authenticatorCode);
    public Task<bool> IsUserExist(User user);
    public Task<EmailAuthenticator> GetByUserId(int Id);

    // Add, Update, Delete

}
