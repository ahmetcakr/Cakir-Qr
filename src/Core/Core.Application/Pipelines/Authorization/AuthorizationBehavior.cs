using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Constants;
using Core.Security.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Core.Application.Pipelines.Authorization;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        List<string>? userRoleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();

        if (userRoleClaims == null)
            throw new AuthorizationException("You are not authenticated.");

        // Kullanıcının rol iddialarından herhangi birinin Admin olup olmadığını veya request.Roles ile eşleşip eşleşmediğini kontrol eder
        bool isMatchedAUserRoleClaimWithRequestRoles = userRoleClaims
            .Any(
                userRoleClaim => userRoleClaim == GeneralOperationClaims.Admin ||
                    request.Roles.Any(role => role == userRoleClaim)
            );

        // Eğer kullanıcı rol iddiası request.Roles ile eşleşmiyorsa yetkisiz hata fırlat
        if (!isMatchedAUserRoleClaimWithRequestRoles)
            throw new AuthorizationException("You are not authorized.");

        TResponse response = await next();
        return response;
    }
}
