using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;
using QrMenu.Application.Repositories;
using QrMenu.Domain.Entities;
using MediatR;

namespace QrMenu.Application.Features.Companies.Queries.GetList;

public class GetListCompanyQuery : IRequest<GetListResponse<GetListCompanyResponse>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public bool BypassCache {get;}

    public string CacheKey => $"GetListCompanies({PageRequest.PageIndex},{PageRequest.PageSize})";

    public string? CacheGroupKey => "GetCompanies";

    public TimeSpan? SlidingExpiration { get; }

    public GetListCompanyQuery()
    {
        PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
    }

    public GetListCompanyQuery(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }

    public GetListCompanyQuery(PageRequest pageRequest, bool bypassCache, TimeSpan? slidingExpiration)
    {
        PageRequest = pageRequest;
        BypassCache = bypassCache;
        SlidingExpiration = slidingExpiration;
    }

    internal sealed class GetListCompanyQueryHandler(
        ICompanyRepository companyRepository,
        IMapper mapper) : IRequestHandler<GetListCompanyQuery, GetListResponse<GetListCompanyResponse>>
    {
        public async Task<GetListResponse<GetListCompanyResponse>> Handle(GetListCompanyQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Company> companies = await companyRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCompanyResponse> response = mapper.Map<GetListResponse<GetListCompanyResponse>>(companies);

            return response;
        }
    }

}
