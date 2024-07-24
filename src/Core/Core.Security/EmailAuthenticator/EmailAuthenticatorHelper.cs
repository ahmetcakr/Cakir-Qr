using System.Security.Cryptography;

namespace Core.Security.EmailAuthenticator;

public class EmailAuthenticatorHelper : IEmailAuthenticatorHelper
{
    public Task<string> CreateEmailActivationKey()
    {
        string shortGuid = Guid.NewGuid().ToString().Substring(0, 8);
        return Task.FromResult(shortGuid);
    }

    public Task<string> CreateEmailActivationCode()
    {
        string code = RandomNumberGenerator
            .GetInt32(Convert.ToInt32(Math.Pow(x: 10, y: 6)))
            .ToString()
            .PadLeft(totalWidth: 6, paddingChar: '0');
        return Task.FromResult(code);
    }
}
