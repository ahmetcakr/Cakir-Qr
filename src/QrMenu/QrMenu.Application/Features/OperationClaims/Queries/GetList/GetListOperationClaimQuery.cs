using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Results;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using QrMenu.Application.Services.OperationClaims;

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

    public class GetListOperationClaimQueryHandler(
        IOperationClaimService _operationClaimService,
        IMapper _mapper
        ): IRequestHandler<GetListOperationClaimQuery, Result<GetListResponse<GetListOperationClaimListItemDto>>>
    {

        public async Task<Result<GetListResponse<GetListOperationClaimListItemDto>>> Handle(
            GetListOperationClaimQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<OperationClaim?>? operationClaims = await _operationClaimService.GetListAsync(
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
