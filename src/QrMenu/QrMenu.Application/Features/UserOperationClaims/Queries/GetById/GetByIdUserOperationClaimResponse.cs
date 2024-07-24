using Core.Application.Responses;

namespace QrMenu.Application.Features.UserOperationClaims.Queries.GetById;

public class GetByIdUserOperationClaimResponse : IResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
}
