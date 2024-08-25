
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Application.Results;
using QrMenu.Domain.Entities;
using QrMenu.Application.Services.MenuQrCodesService;
using MediatR;

namespace QrMenu.Application.Features.MenuQrCodes.Queries.GetList
{
    public class GetListMenuQrCodeQuery : MediatR.IRequest<Result<GetListResponse<GetListMenuQrCodeResponse>>>, ICachableRequest
    {
        public PageRequest PageRequest { get; set; }

        public bool BypassCache { get; }

        public string CacheKey => $"GetListMenuQrCodes({PageRequest.PageIndex},{PageRequest.PageSize})";

        public string? CacheGroupKey => "GetMenuQrCodes";

        public TimeSpan? SlidingExpiration { get; }

        public GetListMenuQrCodeQuery()
        {
            PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
        }

        public GetListMenuQrCodeQuery(PageRequest pageRequest)
        {
            PageRequest = pageRequest;
        }

        public GetListMenuQrCodeQuery(PageRequest pageRequest, bool bypassCache, TimeSpan? slidingExpiration)
        {
            PageRequest = pageRequest;
            BypassCache = bypassCache;
            SlidingExpiration = slidingExpiration;
        }

        internal sealed class GetListMenuQrCodeQueryHandler(
            IMenuQrCodeService menuQrCodeService,
            IMapper mapper) : IRequestHandler<GetListMenuQrCodeQuery, Result<GetListResponse<GetListMenuQrCodeResponse>>>
        {
            public async Task<Result<GetListResponse<GetListMenuQrCodeResponse>>> Handle(GetListMenuQrCodeQuery request, CancellationToken cancellationToken)
            {
                IPaginate<MenuQrCode> menuqrcodes = await menuQrCodeService.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetListMenuQrCodeResponse> response = mapper.Map<GetListResponse<GetListMenuQrCodeResponse>>(menuqrcodes);

                return Result<GetListResponse<GetListMenuQrCodeResponse>>.Succeed(response);
            }
        }
    }
}
