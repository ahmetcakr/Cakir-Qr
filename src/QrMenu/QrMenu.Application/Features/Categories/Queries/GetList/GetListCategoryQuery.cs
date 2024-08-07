
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Application.Results;
using QrMenu.Domain.Entities;
using QrMenu.Application.Services.CategoriesService;
using MediatR;

namespace QrMenu.Application.Features.Categories.Queries.GetList
{
    public class GetListCategoryQuery : MediatR.IRequest<Result<GetListResponse<GetListCategoryResponse>>>, ICachableRequest
    {
        public PageRequest PageRequest { get; set; }

        public bool BypassCache { get; }

        public string CacheKey => $"GetListCategories({PageRequest.PageIndex},{PageRequest.PageSize})";

        public string? CacheGroupKey => "GetCategories";

        public TimeSpan? SlidingExpiration { get; }

        public GetListCategoryQuery()
        {
            PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
        }

        public GetListCategoryQuery(PageRequest pageRequest)
        {
            PageRequest = pageRequest;
        }

        public GetListCategoryQuery(PageRequest pageRequest, bool bypassCache, TimeSpan? slidingExpiration)
        {
            PageRequest = pageRequest;
            BypassCache = bypassCache;
            SlidingExpiration = slidingExpiration;
        }

        internal sealed class GetListCategoryQueryHandler(
            ICategoryService categoryService,
            IMapper mapper) : IRequestHandler<GetListCategoryQuery, Result<GetListResponse<GetListCategoryResponse>>>
        {
            public async Task<Result<GetListResponse<GetListCategoryResponse>>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Category?>? categories = await categoryService.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetListCategoryResponse> response = mapper.Map<GetListResponse<GetListCategoryResponse>>(categories);

                return Result<GetListResponse<GetListCategoryResponse>>.Succeed(response);
            }
        }
    }
}
