using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using QrMenu.Application.Features.OperationClaims.Constants;
using QrMenu.Application.Features.OperationClaims.Rules;
using QrMenu.Application.Repositories;
using MediatR;
using static QrMenu.Application.Features.OperationClaims.Constants.OperationClaimsOperationClaims;
using Core.Application.Results;
using QrMenu.Application.Services.OperationClaims;

namespace QrMenu.Application.Features.OperationClaims.Commands.Update
{
    public class UpdateOperationClaimCommand : IRequest<Result<UpdatedOperationClaimResponse>>, ISecuredRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UpdateOperationClaimCommand()
        {
            Name = string.Empty;
        }

        public UpdateOperationClaimCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string[] Roles => new[] { Admin, Write, OperationClaimsOperationClaims.Update };

        public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, Result<UpdatedOperationClaimResponse>>
        {
            private readonly IOperationClaimService _operationClaimService;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public UpdateOperationClaimCommandHandler(
                IOperationClaimService operationClaimService,
                IMapper mapper,
                OperationClaimBusinessRules operationClaimBusinessRules
            )
            {
                _operationClaimService = operationClaimService;
                _mapper = mapper;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<Result<UpdatedOperationClaimResponse>> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                OperationClaim? operationClaim = await _operationClaimService.GetAsync(
                    predicate: oc => oc.Id == request.Id,
                    cancellationToken: cancellationToken
                );
                await _operationClaimBusinessRules.OperationClaimShouldExistWhenSelected(operationClaim);
                await _operationClaimBusinessRules.OperationClaimNameShouldNotExistWhenUpdating(request.Id, request.Name);
                OperationClaim mappedOperationClaim = _mapper.Map(request, destination: operationClaim!);

                OperationClaim updatedOperationClaim = await _operationClaimService.UpdateAsync(mappedOperationClaim);

                UpdatedOperationClaimResponse response = _mapper.Map<UpdatedOperationClaimResponse>(updatedOperationClaim);
                return Result<UpdatedOperationClaimResponse>.Succeed(response);
            }
        }
    }
}
