using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using QrMenu.Application.Features.Auth.Constants;
using QrMenu.Application.Repositories;

namespace QrMenu.Application.Features.Auth.Rules;

public class AuthBusinessRules : BaseBusinessRules
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;

    public AuthBusinessRules(IUserRepository userRepository, IEmailAuthenticatorRepository emailAuthenticatorRepository)
    {
        _userRepository = userRepository;
        _emailAuthenticatorRepository = emailAuthenticatorRepository;
    }

    public async Task EmailAuthenticatorShouldBeVerify(int userId)
    {
        var emailAuth = await _emailAuthenticatorRepository.GetByUserIdAsync(userId);

        if (!emailAuth.IsVerified)
            throw new BusinessException("Email is not verified.");

        return;
    }

    public Task EmailAuthenticatorShouldBeExists(EmailAuthenticator? emailAuthenticator)
    {
        if (emailAuthenticator is null)
            throw new BusinessException(AuthMessages.EmailAuthenticatorDontExists);
        return Task.CompletedTask;
    }

    public Task OtpAuthenticatorShouldBeExists(OtpAuthenticator? otpAuthenticator)
    {
        if (otpAuthenticator is null)
            throw new BusinessException(AuthMessages.OtpAuthenticatorDontExists);
        return Task.CompletedTask;
    }

    public Task OtpAuthenticatorThatVerifiedShouldNotBeExists(OtpAuthenticator? otpAuthenticator)
    {
        if (otpAuthenticator is not null && otpAuthenticator.IsVerified)
            throw new BusinessException(AuthMessages.AlreadyVerifiedOtpAuthenticatorIsExists);
        return Task.CompletedTask;
    }

    public Task EmailAuthenticatorActivationKeyShouldBeExists(EmailAuthenticator emailAuthenticator)
    {
        if (emailAuthenticator.ActivationKey is null)
            throw new BusinessException(AuthMessages.EmailActivationKeyDontExists);
        return Task.CompletedTask;
    }

    public Task UserShouldBeExistsWhenSelected(User? user)
    {
        if (user == null)
            throw new BusinessException(AuthMessages.UserDontExists);
        return Task.CompletedTask;
    }

    public Task RefreshTokenShouldBeExists(RefreshToken? refreshToken)
    {
        if (refreshToken == null)
            throw new BusinessException(AuthMessages.RefreshDontExists);
        return Task.CompletedTask;
    }

    public Task RefreshTokenShouldBeActive(RefreshToken refreshToken)
    {
        if (refreshToken.Revoked != null && DateTime.UtcNow >= refreshToken.Expires)
            throw new BusinessException(AuthMessages.InvalidRefreshToken);
        return Task.CompletedTask;
    }

    public async Task UserEmailShouldBeNotExists(string email)
    {
        bool doesExists = await _userRepository.AnyAsync(predicate: u => u.Email == email, enableTracking: false);
        if (doesExists)
            throw new BusinessException(AuthMessages.UserMailAlreadyExists);
    }

    public async Task UserPasswordShouldBeMatch(int id, string password)
    {
        User? user = await _userRepository.GetAsync(predicate: u => u.Id == id, enableTracking: false);
        await UserShouldBeExistsWhenSelected(user);
        if (!HashingHelper.VerifyPasswordHash(password, user!.PasswordHash, user.PasswordSalt))
            throw new BusinessException(AuthMessages.PasswordDontMatch);
    }
}
