using Core.Application.Responses;
using Core.Security.Enums;
using Core.Security.JWT;

namespace QrMenu.Application.Features.Auth.Commands.Login;

public class LoggedResponse : IResponse
{
    public AccessToken? AccessToken { get; set; }
    public Core.Security.Entities.RefreshToken? RefreshToken { get; set; }

    public LoggedHttpResponse ToHttpResponse() =>
        new() { AccessToken = AccessToken };

    public class LoggedHttpResponse
    {
        public AccessToken? AccessToken { get; set; }
    }
}
