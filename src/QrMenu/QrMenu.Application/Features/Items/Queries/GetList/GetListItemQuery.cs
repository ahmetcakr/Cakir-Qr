
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Application.Results;
using QrMenu.Domain.Entities;
using QrMenu.Application.Services.ItemsService;
using MediatR;

namespace QrMenu.Application.Features.Items.Queries.GetList
{
    public class GetListItemQuery : MediatR.IRequest<Result<GetListResponse<GetListItemResponse>>>, ICachableRequest
    {
        public PageRequest PageRequest { get; set; }

        public bool BypassCache { get; }

        public string CacheKey => $"GetListItems({PageRequest.PageIndex},{PageRequest.PageSize})";

        public string? CacheGroupKey => "GetItems";

        public TimeSpan? SlidingExpiration { get; }

        public GetListItemQuery()
        {
            PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
        }

        public GetListItemQuery(PageRequest pageRequest)
        {
            PageRequest = pageRequest;
        }

        public GetListItemQuery(PageRequest pageRequest, bool bypassCache, TimeSpan? slidingExpiration)
        {
            PageRequest = pageRequest;
            BypassCache = bypassCache;
            SlidingExpiration = slidingExpiration;
        }

        internal sealed class GetListItemQueryHandler(
            IItemService ıtemService,
            IMapper mapper) : IRequestHandler<GetListItemQuery, Result<GetListResponse<GetListItemResponse>>>
        {
            public async Task<Result<GetListResponse<GetListItemResponse>>> Handle(GetListItemQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Item> ıtems = await ıtemService.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetListItemResponse> response = mapper.Map<GetListResponse<GetListItemResponse>>(ıtems);

                return Result<GetListResponse<GetListItemResponse>>.Succeed(response);
            }
        }
    }
}
