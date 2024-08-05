using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Results;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;
using QrMenu.Application.Features.Companies.Constants;
using QrMenu.Application.Features.Companies.Rules;
using QrMenu.Application.Services.CompaniesService;
using QrMenu.Domain.Entities;
using static QrMenu.Application.Features.Companies.Constants.CompaniesOperationClaims;

namespace QrMenu.Application.Features.Companies.Commands.Delete;

public class DeleteCompanyCommand : IRequest<Result<DeletedCompanyResponse>>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
    {
        Admin,
        CompaniesOperationClaims.Delete,
        Write
    };

    public class DeleteCompanyCommandHandler(
        ICompanyService _companyService,
        CompanyBusinessRules companyBusinessRules,
        IMapper mapper) : IRequestHandler<DeleteCompanyCommand, Result<DeletedCompanyResponse>>
    {
        public async Task<Result<DeletedCompanyResponse>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            await companyBusinessRules.CompanyIdShouldBeExist(request.Id);

            Company company = await _companyService.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

            if (company is null)
                throw new BusinessException("Company does not exist.");

            await _companyService.DeleteAsync(company);

            DeletedCompanyResponse response = mapper.Map<DeletedCompanyResponse>(company);

            return Result<DeletedCompanyResponse>.Succeed(response);
        }
    }
}
