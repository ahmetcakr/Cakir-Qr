using Core.Application.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.JWT;
using QrMenu.Application.Features.Auth.Rules;
using QrMenu.Application.Services.AuthenticatorService;
using QrMenu.Application.Services.AuthService;
using QrMenu.Application.Services.UsersService;
using MediatR;
using Core.Application.Results;

namespace QrMenu.Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<Result<LoggedResponse>>
{
    public UserForLoginDto UserForLoginDto { get; set; }
    public string IpAddress { get; set; }

    public LoginCommand()
    {
        UserForLoginDto = null!;
        IpAddress = string.Empty;
    }

    public LoginCommand(UserForLoginDto userForLoginDto, string ipAddress)
    {
        UserForLoginDto = userForLoginDto;
        IpAddress = ipAddress;
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoggedResponse>>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public LoginCommandHandler(
            IUserService userService,
            IAuthService authService,
            AuthBusinessRules authBusinessRules,
            IAuthenticatorService authenticatorService
        )
        {
            _userService = userService;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
        }

        public async Task<Result<LoggedResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetAsync(
                predicate: u => u.Email == request.UserForLoginDto.Email,
                cancellationToken: cancellationToken
            );

            await _authBusinessRules.UserShouldBeExistsWhenSelected(user);
            await _authBusinessRules.UserPasswordShouldBeMatch(user!.Id, request.UserForLoginDto.Password);

            LoggedResponse loggedResponse = new();

            if(!await _authenticatorService.IsUserExist(user)) 
            {
                await _authenticatorService.CreateEmailAuthenticator(user);

                await _authenticatorService.SendAuthenticatorCode(user);

                return Result<LoggedResponse>.Succeed(loggedResponse);
            }
            else
            {
                await _authBusinessRules.EmailAuthenticatorShouldBeVerify(user!.Id);
            }

            AccessToken createdAccessToken = await _authService.CreateAccessToken(user);

            Core.Security.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IpAddress);
            Core.Security.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
            await _authService.DeleteOldRefreshTokens(user.Id);

            loggedResponse.AccessToken = createdAccessToken;
            loggedResponse.RefreshToken = addedRefreshToken;

            return Result<LoggedResponse>.Succeed(loggedResponse);
        }
    }
}
