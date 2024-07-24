using Core.Mailing;
using Core.Security.Entities;
using Core.Security.Enums;
using QrMenu.Application.Features.Auth.Rules;
using QrMenu.Application.Repositories;
using QrMenu.Application.Services.AuthenticatorService;
using QrMenu.Application.Services.UsersService;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QrMenu.Application.Features.Auth.Commands.EnableEmailAuthenticator;

public class EnableEmailAuthenticatorCommand : IRequest
{
    public int UserId { get; set; }
    public string VerifyEmailUrlPrefix { get; set; }

    public EnableEmailAuthenticatorCommand()
    {
        VerifyEmailUrlPrefix = string.Empty;
    }

    public EnableEmailAuthenticatorCommand(int userId, string verifyEmailUrlPrefix)
    {
        UserId = userId;
        VerifyEmailUrlPrefix = verifyEmailUrlPrefix;
    }

    public class EnableEmailAuthenticatorCommandHandler : IRequestHandler<EnableEmailAuthenticatorCommand>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
        private readonly IMailService _mailService;
        private readonly IUserService _userService;

        public EnableEmailAuthenticatorCommandHandler(
            IUserService userService,
            IEmailAuthenticatorRepository emailAuthenticatorRepository,
            IMailService mailService,
            AuthBusinessRules authBusinessRules,
            IAuthenticatorService authenticatorService
        )
        {
            _userService = userService;
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
            _mailService = mailService;
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
        }

        public async Task Handle(EnableEmailAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetAsync(predicate: u => u.Id == request.UserId, cancellationToken: cancellationToken);
            await _authBusinessRules.UserShouldBeExistsWhenSelected(user);
            await _authBusinessRules.UserShouldNotBeHaveAuthenticator(user!);

            user!.AuthenticatorType = AuthenticatorType.Email;
            await _userService.UpdateAsync(user);

            EmailAuthenticator emailAuthenticator = await _authenticatorService.CreateEmailAuthenticator(user);
            EmailAuthenticator addedEmailAuthenticator = await _emailAuthenticatorRepository.AddAsync(emailAuthenticator);

            var toEmailList = new List<MailboxAddress> { new(name: $"{user.FirstName} {user.LastName}", user.Email) };

            _mailService.SendMail(
                new Mail
                {
                    ToList = toEmailList,
                    Subject = "Verify Your Email - NArchitecture",
                    TextBody =
                        $"Click on the link to verify your email: {request.VerifyEmailUrlPrefix}?ActivationKey={HttpUtility.UrlEncode(addedEmailAuthenticator.ActivationKey)}"
                }
            );
        }
    }
}
