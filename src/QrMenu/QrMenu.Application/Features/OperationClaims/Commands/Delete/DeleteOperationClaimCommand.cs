using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using QrMenu.Application.Features.OperationClaims.Constants;
using QrMenu.Application.Features.OperationClaims.Rules;
using QrMenu.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static QrMenu.Application.Features.OperationClaims.Constants.OperationClaimsOperationClaims;
using Core.Application.Results;
using QrMenu.Application.Services.OperationClaims;

namespace QrMenu.Application.Features.OperationClaims.Commands.Delete;

public class DeleteOperationClaimCommand : IRequest<Result<DeletedOperationClaimResponse>>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, OperationClaimsOperationClaims.Delete };

    public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, Result<DeletedOperationClaimResponse>>
    {
        private readonly IOperationClaimService _operationClaimService;
        private readonly IMapper _mapper;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public DeleteOperationClaimCommandHandler(
            IOperationClaimService operationClaimService,
            IMapper mapper,
            OperationClaimBusinessRules operationClaimBusinessRules
        )
        {
            _operationClaimService = operationClaimService;
            _mapper = mapper;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<Result<DeletedOperationClaimResponse>> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
        {
            OperationClaim? operationClaim = await _operationClaimService.GetAsync(
                predicate: oc => oc.Id == request.Id,
                include: q => q.Include(oc => oc.UserOperationClaims),
                cancellationToken: cancellationToken
            );
            await _operationClaimBusinessRules.OperationClaimShouldExistWhenSelected(operationClaim);

            await _operationClaimService.DeleteAsync(operationClaim!);

            DeletedOperationClaimResponse response = _mapper.Map<DeletedOperationClaimResponse>(operationClaim);
            return Result<DeletedOperationClaimResponse>.Succeed(response);
        }
    }
}
