
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Application.Results;
using QrMenu.Domain.Entities;
using QrMenu.Application.Services.ItemImagesService;
using MediatR;

namespace QrMenu.Application.Features.ItemImages.Queries.GetList
{
    public class GetListItemImageQuery : MediatR.IRequest<Result<GetListResponse<GetListItemImageResponse>>>, ICachableRequest
    {
        public PageRequest PageRequest { get; set; }

        public bool BypassCache { get; }

        public string CacheKey => $"GetListItemImages({PageRequest.PageIndex},{PageRequest.PageSize})";

        public string? CacheGroupKey => "GetItemImages";

        public TimeSpan? SlidingExpiration { get; }

        public GetListItemImageQuery()
        {
            PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
        }

        public GetListItemImageQuery(PageRequest pageRequest)
        {
            PageRequest = pageRequest;
        }

        public GetListItemImageQuery(PageRequest pageRequest, bool bypassCache, TimeSpan? slidingExpiration)
        {
            PageRequest = pageRequest;
            BypassCache = bypassCache;
            SlidingExpiration = slidingExpiration;
        }

        internal sealed class GetListItemImageQueryHandler(
            IItemImageService ıtemImageService,
            IMapper mapper) : IRequestHandler<GetListItemImageQuery, Result<GetListResponse<GetListItemImageResponse>>>
        {
            public async Task<Result<GetListResponse<GetListItemImageResponse>>> Handle(GetListItemImageQuery request, CancellationToken cancellationToken)
            {
                IPaginate<ItemImage> ıtemımages = await ıtemImageService.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetListItemImageResponse> response = mapper.Map<GetListResponse<GetListItemImageResponse>>(ıtemımages);

                return Result<GetListResponse<GetListItemImageResponse>>.Succeed(response);
            }
        }
    }
}
