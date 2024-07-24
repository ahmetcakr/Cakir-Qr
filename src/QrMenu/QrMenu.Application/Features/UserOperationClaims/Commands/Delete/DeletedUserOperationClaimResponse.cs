using Core.Application.Responses;

namespace QrMenu.Application.Features.UserOperationClaims.Commands.Delete;

public class DeletedUserOperationClaimResponse : IResponse
{
    public int Id { get; set; }
}
