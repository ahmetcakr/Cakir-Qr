using AutoMapper;
using Core.Security.Entities;
using QrMenu.Application.Features.OperationClaims.Rules;
using QrMenu.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Core.Application.Results;
using QrMenu.Application.Services.OperationClaims;

namespace QrMenu.Application.Features.OperationClaims.Queries.GetById;

public class GetByIdOperationClaimQuery : IRequest<Result<GetByIdOperationClaimResponse>>
{
    public int Id { get; set; }

    public class GetByIdOperationClaimQueryHandler(
        IOperationClaimService _operationClaimService,
        IMapper _mapper,
        OperationClaimBusinessRules _operationClaimBusinessRules) : IRequestHandler<GetByIdOperationClaimQuery, Result<GetByIdOperationClaimResponse>>
    {
        public async Task<Result<GetByIdOperationClaimResponse>> Handle(GetByIdOperationClaimQuery request, CancellationToken cancellationToken)
        {
            OperationClaim? operationClaim = await _operationClaimService.GetAsync(
                predicate: b => b.Id == request.Id,
                include: q => q.Include(oc => oc.UserOperationClaims),
                cancellationToken: cancellationToken
            );
            await _operationClaimBusinessRules.OperationClaimShouldExistWhenSelected(operationClaim);

            GetByIdOperationClaimResponse response = _mapper.Map<GetByIdOperationClaimResponse>(operationClaim);
            return Result<GetByIdOperationClaimResponse>.Succeed(response);
        }
    }
}
