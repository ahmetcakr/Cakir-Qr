using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using QrMenu.Application.Features.Auth.Rules;
using QrMenu.Application.Repositories;
using MediatR;

namespace QrMenu.Application.Features.Auth.Commands.VerifyEmailAuthenticator;

public class VerifyEmailAuthenticatorCommand : IRequest
{
    public int UserId { get; set; }
    public string ActivationKey { get; set; }

    public VerifyEmailAuthenticatorCommand()
    {
        ActivationKey = string.Empty;
        UserId = 0;
    }

    public VerifyEmailAuthenticatorCommand(string activationKey, int userId)
    {
        ActivationKey = activationKey;
        UserId = userId;
    }

    public class VerifyEmailAuthenticatorCommandHandler : IRequestHandler<VerifyEmailAuthenticatorCommand>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;

        public VerifyEmailAuthenticatorCommandHandler(
            IEmailAuthenticatorRepository emailAuthenticatorRepository,
            AuthBusinessRules authBusinessRules
        )
        {
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
            _authBusinessRules = authBusinessRules;
        }

        public async Task Handle(VerifyEmailAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            var emailAuth = await _emailAuthenticatorRepository.GetByUserIdAsync(request.UserId);

            if (emailAuth == null)
                throw new NotFoundException(nameof(EmailAuthenticator));

            if (emailAuth.ActivationKey != request.ActivationKey)
                throw new Exception("Activation key is not valid.");

            emailAuth.ActivationKey = "";
            emailAuth.IsVerified = true;


            await _emailAuthenticatorRepository.UpdateAsync(emailAuth);

            return;
        }
    }
}
