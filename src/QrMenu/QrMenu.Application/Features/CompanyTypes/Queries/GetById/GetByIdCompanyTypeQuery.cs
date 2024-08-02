using AutoMapper;
using QrMenu.Application.Features.CompanyTypes.Rules;
using QrMenu.Application.Repositories;
using MediatR;
using Core.Application.Results;

namespace QrMenu.Application.Features.CompanyTypes.Queries.GetById;

public class GetByIdCompanyTypeQuery : IRequest<Result<GetByIdCompanyTypeResponse>>
{
    public int Id { get; set; }

    internal sealed class GetByIdCompanyTypeQueryHandler(
        ICompanyTypeRepository companyTypeRepository,
        IMapper mapper,
        CompanyTypeBusinessRules companyTypeBusinessRules) : IRequestHandler<GetByIdCompanyTypeQuery, Result<GetByIdCompanyTypeResponse>>
    {

        public async Task<Result<GetByIdCompanyTypeResponse>> Handle(GetByIdCompanyTypeQuery request, CancellationToken cancellationToken)
        {
            var company = await companyTypeRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            GetByIdCompanyTypeResponse getByIdCompanyTypeResponse = mapper.Map<GetByIdCompanyTypeResponse>(company);

            return Result<GetByIdCompanyTypeResponse>.Succeed(getByIdCompanyTypeResponse);
        }
    }
}
