using QrMenu.Application.Features.UserOperationClaims.Constants;
using QrMenu.Application.Features.UserOperationClaims.Rules;
using QrMenu.Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using static QrMenu.Application.Features.UserOperationClaims.Constants.UserOperationClaimsOperationClaims;
using Core.Application.Results;

namespace QrMenu.Application.Features.UserOperationClaims.Commands.Update;

public class UpdateUserOperationClaimCommand : IRequest<Result<UpdatedUserOperationClaimResponse>>, ISecuredRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public string[] Roles => new[] { Admin, Write, UserOperationClaimsOperationClaims.Update };

    public class UpdateUserOperationClaimCommandHandler
        : IRequestHandler<UpdateUserOperationClaimCommand, Result<UpdatedUserOperationClaimResponse>>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public UpdateUserOperationClaimCommandHandler(
            IUserOperationClaimRepository userOperationClaimRepository,
            IMapper mapper,
            UserOperationClaimBusinessRules userOperationClaimBusinessRules
        )
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<Result<UpdatedUserOperationClaimResponse>> Handle(
            UpdateUserOperationClaimCommand request,
            CancellationToken cancellationToken
        )
        {
            UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(
                predicate: uoc => uoc.Id == request.Id,
                enableTracking: false,
                cancellationToken: cancellationToken
            );
            await _userOperationClaimBusinessRules.UserOperationClaimShouldExistWhenSelected(userOperationClaim);
            await _userOperationClaimBusinessRules.UserShouldNotHasOperationClaimAlreadyWhenUpdated(
                request.Id,
                request.UserId,
                request.OperationClaimId
            );
            UserOperationClaim mappedUserOperationClaim = _mapper.Map(request, destination: userOperationClaim!);

            UserOperationClaim updatedUserOperationClaim = await _userOperationClaimRepository.UpdateAsync(mappedUserOperationClaim);

            UpdatedUserOperationClaimResponse updatedUserOperationClaimDto = _mapper.Map<UpdatedUserOperationClaimResponse>(
                updatedUserOperationClaim
            );

            return Result<UpdatedUserOperationClaimResponse>.Succeed(updatedUserOperationClaimDto);
        }
    }
}
