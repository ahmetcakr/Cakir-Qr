using AutoMapper;
using Core.Security.Entities;
using QrMenu.Application.Features.OperationClaims.Rules;
using QrMenu.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Core.Application.Results;

namespace QrMenu.Application.Features.OperationClaims.Queries.GetById;

public class GetByIdOperationClaimQuery : IRequest<Result<GetByIdOperationClaimResponse>>
{
    public int Id { get; set; }

    public class GetByIdOperationClaimQueryHandler : IRequestHandler<GetByIdOperationClaimQuery, Result<GetByIdOperationClaimResponse>>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public GetByIdOperationClaimQueryHandler(
            IOperationClaimRepository operationClaimRepository,
            IMapper mapper,
            OperationClaimBusinessRules operationClaimBusinessRules
        )
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<Result<GetByIdOperationClaimResponse>> Handle(GetByIdOperationClaimQuery request, CancellationToken cancellationToken)
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(
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
