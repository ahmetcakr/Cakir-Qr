using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using QrMenu.Application.Features.Auth.Rules;
using QrMenu.Application.Repositories;
using QrMenu.Application.Services.AuthenticatorService;
using MediatR;
using RabbitMQ.Client.Events;

namespace QrMenu.Application.Features.Auth.Commands.SendAuthCode
{
    public class SendAuthCodeCommand : IRequest
    {
        public int UserId { get; set; }

        public SendAuthCodeCommand(int userId)
        {
            UserId = userId;
        }

        public SendAuthCodeCommand()
        {
            UserId = 0;
        }

        public class SendAuthCodeCommandHandler(
               IAuthenticatorService _authenticatorService,
               IEmailAuthenticatorRepository _emailAuthenticatorRepository) : IRequestHandler<SendAuthCodeCommand>
        {
            public async Task Handle(SendAuthCodeCommand request, CancellationToken cancellationToken)
            {
                var emailAuth = await _emailAuthenticatorRepository.GetByUserIdAsync(request.UserId);

                if (emailAuth == null)
                    throw new NotFoundException(nameof(EmailAuthenticator));

                if (emailAuth.IsVerified)
                    throw new BusinessException("Email is already verified.");

                await _authenticatorService.SendAuthenticatorCode(emailAuth.UserId);

                return;
            }
        }
    }
}
