using AutoMapper;
using QrMenu.Application.Features.Companies.Rules;
using QrMenu.Application.Repositories;
using MediatR;
using Core.Application.Results;
using QrMenu.Application.Services.CompaniesService;

namespace QrMenu.Application.Features.Companies.Queries.GetById;

public class GetByIdCompanyQuery : IRequest<Result<GetByIdCompanyResponse>>
{
    public int Id { get; set; }

    internal sealed class GetByIdCompanyQueryHandler(
        ICompanyService _companyService,
        IMapper mapper,
        CompanyBusinessRules companyBusinessRules) : IRequestHandler<GetByIdCompanyQuery, Result<GetByIdCompanyResponse>>
    {

        public async Task<Result<GetByIdCompanyResponse>> Handle(GetByIdCompanyQuery request, CancellationToken cancellationToken)
        {
            var company = await _companyService.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            GetByIdCompanyResponse getByIdCompanyResponse = mapper.Map<GetByIdCompanyResponse>(company);

            return Result<GetByIdCompanyResponse>.Succeed(getByIdCompanyResponse);
        }
    }
}
