using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;
using QrMenu.Application.Repositories;
using MediatR;
using Core.Application.Results;

namespace QrMenu.Application.Features.OperationClaims.Queries.GetList;

public class GetListOperationClaimQuery : IRequest<Result<GetListResponse<GetListOperationClaimListItemDto>>>
{
    public PageRequest PageRequest { get; set; }

    public GetListOperationClaimQuery()
    {
        PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
    }

    public GetListOperationClaimQuery(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }

    public class GetListOperationClaimQueryHandler
        : IRequestHandler<GetListOperationClaimQuery, Result<GetListResponse<GetListOperationClaimListItemDto>>>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;

        public GetListOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetListResponse<GetListOperationClaimListItemDto>>> Handle(
            GetListOperationClaimQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<OperationClaim> operationClaims = await _operationClaimRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListOperationClaimListItemDto> response = _mapper.Map<GetListResponse<GetListOperationClaimListItemDto>>(
                operationClaims
            );
            return Result<GetListResponse<GetListOperationClaimListItemDto>>.Succeed(response);
        }
    }
}
