
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Application.Results;
using QrMenu.Domain.Entities;
using QrMenu.Application.Services.MenusService;
using MediatR;

namespace QrMenu.Application.Features.Menus.Queries.GetList
{
    public class GetListMenuQuery : MediatR.IRequest<Result<GetListResponse<GetListMenuResponse>>>, ICachableRequest
    {
        public PageRequest PageRequest { get; set; }

        public bool BypassCache { get; }

        public string CacheKey => $"GetListMenus({PageRequest.PageIndex},{PageRequest.PageSize})";

        public string? CacheGroupKey => "GetMenus";

        public TimeSpan? SlidingExpiration { get; }

        public GetListMenuQuery()
        {
            PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
        }

        public GetListMenuQuery(PageRequest pageRequest)
        {
            PageRequest = pageRequest;
        }

        public GetListMenuQuery(PageRequest pageRequest, bool bypassCache, TimeSpan? slidingExpiration)
        {
            PageRequest = pageRequest;
            BypassCache = bypassCache;
            SlidingExpiration = slidingExpiration;
        }

        internal sealed class GetListMenuQueryHandler(
            IMenuService menuService,
            IMapper mapper) : IRequestHandler<GetListMenuQuery, Result<GetListResponse<GetListMenuResponse>>>
        {
            public async Task<Result<GetListResponse<GetListMenuResponse>>> Handle(GetListMenuQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Menu> menus = await menuService.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetListMenuResponse> response = mapper.Map<GetListResponse<GetListMenuResponse>>(menus);

                return Result<GetListResponse<GetListMenuResponse>>.Succeed(response);
            }
        }
    }
}
