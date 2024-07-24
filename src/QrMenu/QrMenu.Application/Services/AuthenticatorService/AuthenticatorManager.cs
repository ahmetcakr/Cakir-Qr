using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Mailing;
using Core.Security.EmailAuthenticator;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.OtpAuthenticator;
using QrMenu.Application.Repositories;
using MimeKit;

namespace QrMenu.Application.Services.AuthenticatorService;

public class AuthenticatorManager : IAuthenticatorService
{
    private readonly IEmailAuthenticatorHelper _emailAuthenticatorHelper;
    private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
    private readonly IMailService _mailService;
    private readonly IOtpAuthenticatorHelper _otpAuthenticatorHelper;
    private readonly IOtpAuthenticatorRepository _otpAuthenticatorRepository;
    private readonly IUserRepository _userRepository;

    public AuthenticatorManager(
        IEmailAuthenticatorHelper emailAuthenticatorHelper,
        IEmailAuthenticatorRepository emailAuthenticatorRepository,
        IMailService mailService,
        IOtpAuthenticatorHelper otpAuthenticatorHelper,
        IOtpAuthenticatorRepository otpAuthenticatorRepository,
        IUserRepository userRepository
    )
    {
        _emailAuthenticatorHelper = emailAuthenticatorHelper;
        _emailAuthenticatorRepository = emailAuthenticatorRepository;
        _mailService = mailService;
        _otpAuthenticatorHelper = otpAuthenticatorHelper;
        _otpAuthenticatorRepository = otpAuthenticatorRepository;
        _userRepository = userRepository;
    }

    public async Task<EmailAuthenticator> CreateEmailAuthenticator(User user)
    {
        EmailAuthenticator emailAuthenticator =
            new()
            {
                UserId = user.Id,
                ActivationKey = await _emailAuthenticatorHelper.CreateEmailActivationKey(),
                IsVerified = false
            };

        await _emailAuthenticatorRepository.AddAsync(emailAuthenticator);
        return emailAuthenticator;
    }

    public async Task<OtpAuthenticator> CreateOtpAuthenticator(User user)
    {
        OtpAuthenticator otpAuthenticator =
            new()
            {
                UserId = user.Id,
                SecretKey = await _otpAuthenticatorHelper.GenerateSecretKey(),
                IsVerified = false
            };
        return otpAuthenticator;
    }

    public async Task<string> ConvertSecretKeyToString(byte[] secretKey)
    {
        string result = await _otpAuthenticatorHelper.ConvertSecretKeyToString(secretKey);
        return result;
    }

    public async Task SendAuthenticatorCode(User user)
    {
        await SendAuthenticatorCodeWithEmail(user);
    }

    public async Task SendAuthenticatorCode(int userId)
    {
        User user = await _userRepository.GetAsync(x => x.Id == userId);
        await SendAuthenticatorCodeWithEmail(user);
    }

    public async Task VerifyAuthenticatorCode(User user, string authenticatorCode)
    {
        await VerifyAuthenticatorCodeWithEmail(user, authenticatorCode);
    }

    private async Task SendAuthenticatorCodeWithEmail(User user)
    {
        EmailAuthenticator? emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(predicate: e => e.UserId == user.Id);
        if (emailAuthenticator is null)
            throw new NotFoundException("Email Authenticator not found.");

        string authenticatorCode = emailAuthenticator.ActivationKey ?? "123456789";

        var toEmailList = new List<MailboxAddress> { new(name: $"{user.FirstName} {user.LastName}", user.Email) };

        _mailService.SendMail(
            new Mail
            {
                ToList = toEmailList,
                Subject = "Authenticator Code - CakirQrMenu",
                TextBody = $"Enter your authenticator code: {authenticatorCode}"
            }
        );
    }

    private async Task VerifyAuthenticatorCodeWithEmail(User user, string authenticatorCode)
    {
        EmailAuthenticator? emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(predicate: e => e.UserId == user.Id);
        if (emailAuthenticator is null)
            throw new NotFoundException("Email Authenticator not found.");
        if (emailAuthenticator.ActivationKey != authenticatorCode)
            throw new BusinessException("Authenticator code is invalid.");
        emailAuthenticator.ActivationKey = null;
        await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);
    }

    private async Task VerifyAuthenticatorCodeWithOtp(User user, string authenticatorCode)
    {
        OtpAuthenticator? otpAuthenticator = await _otpAuthenticatorRepository.GetAsync(predicate: e => e.UserId == user.Id);
        if (otpAuthenticator is null)
            throw new NotFoundException("Otp Authenticator not found.");
        bool result = await _otpAuthenticatorHelper.VerifyCode(otpAuthenticator.SecretKey, authenticatorCode);
        if (!result)
            throw new BusinessException("Authenticator code is invalid.");
    }

    public async Task<EmailAuthenticator> VerifyEmailAuthenticator(EmailAuthenticator emailAuthenticator)
    {
        EmailAuthenticator verifiedEmail = await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);

        return verifiedEmail;
    }

    public async Task<bool> IsUserExist(User user)
    {
        return await _emailAuthenticatorRepository.GetByUserIdAsync(user.Id) is not null;
    }

    public async Task<EmailAuthenticator> GetByUserId(int Id)
    {
        return await _emailAuthenticatorRepository.GetByUserIdAsync(Id);
    }
}
