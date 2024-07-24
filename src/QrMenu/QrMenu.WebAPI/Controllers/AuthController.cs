using Core.Application.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using QrMenu.Application.Features.Auth.Commands.VerifyEmailAuthenticator;
using QrMenu.Application.Features.Auth.Commands.Login;
using QrMenu.Application.Features.Auth.Commands.Register;
using QrMenu.Application.Features.Auth.Commands.RefreshToken;
using QrMenu.Application.Features.Auth.Commands.RevokeToken;
using QrMenu.Application.Features.Auth.Commands.SendAuthCode;

namespace QrMenu.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    private readonly WebApiConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        const string configurationSection = "WebApiConfiguration";
        _configuration =
            configuration.GetSection(configurationSection).Get<WebApiConfiguration>()
            ?? throw new NullReferenceException($"\"{configurationSection}\" section cannot found in configuration.");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
    {
        LoginCommand loginCommand = new() { UserForLoginDto = userForLoginDto, IpAddress = getIpAddress() };
        LoggedResponse result = await Mediator.Send(loginCommand);

        if (result.RefreshToken is not null)
            setRefreshTokenToCookie(result.RefreshToken);

        return Ok(result.ToHttpResponse());
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
    {
        RegisterCommand registerCommand = new() { UserForRegisterDto = userForRegisterDto, IpAddress = getIpAddress() };
        RegisteredResponse result = await Mediator.Send(registerCommand);
        return Created(getIpAddress(), result.Message);
    }

    [HttpGet("RefreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        RefreshTokenCommand refreshTokenCommand = new() { RefreshToken = getRefreshTokenFromCookies(), IpAddress = getIpAddress() };
        RefreshedTokensResponse result = await Mediator.Send(refreshTokenCommand);
        setRefreshTokenToCookie(result.RefreshToken);
        return Created(uri: "", result.AccessToken);
    }

    [HttpPut("RevokeToken")]
    public async Task<IActionResult> RevokeToken([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] string? refreshToken)
    {
        RevokeTokenCommand revokeTokenCommand = new() { Token = refreshToken ?? getRefreshTokenFromCookies(), IpAddress = getIpAddress() };
        RevokedTokenResponse result = await Mediator.Send(revokeTokenCommand);
        return Ok(result);
    }


    [HttpGet("VerifyEmailAuthenticator")]
    public async Task<IActionResult> VerifyEmailAuthenticator([FromQuery] VerifyEmailAuthenticatorCommand verifyEmailAuthenticatorCommand)
    {
        await Mediator.Send(verifyEmailAuthenticatorCommand);
        return Ok();
    }

    [HttpGet("SendAuthenticateCode")]
    public async Task<IActionResult> SendAuthenticateCode([FromQuery] int userId)
    {
        await Mediator.Send(new SendAuthCodeCommand(userId));
        return Ok();
    }

    private string getRefreshTokenFromCookies() =>
        Request.Cookies["refreshToken"] ?? throw new ArgumentException("Refresh token is not found in request cookies.");

    private void setRefreshTokenToCookie(RefreshToken refreshToken)
    {
        CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.UtcNow.AddDays(7) };
        Response.Cookies.Append(key: "refreshToken", refreshToken.Token, cookieOptions);
    }
}
