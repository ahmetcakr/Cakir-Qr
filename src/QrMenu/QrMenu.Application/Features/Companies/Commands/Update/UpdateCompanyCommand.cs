using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Results;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;
using QrMenu.Application.Features.Companies.Constants;
using QrMenu.Application.Features.Companies.Rules;
using QrMenu.Application.Services.CompaniesService;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Features.Companies.Commands.Update;

public class UpdateCompanyCommand : IRequest<Result<UpdatedCompanyResponse>>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public int CompanyTypeId { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }

    public string[] Roles => new[]
    {
        CompaniesOperationClaims.Admin,
        CompaniesOperationClaims.Write,
        CompaniesOperationClaims.Update
    };

    public bool BypassCache => false;
    public string? CacheKey => "";
    public string? CacheGroupKey => "GetCompanies";

    public UpdateCompanyCommand()
    {
        Id = 0;
        CompanyName = string.Empty;
        CompanyTypeId = 0;
        Address = string.Empty;
        Phone = string.Empty;
        Email = string.Empty;
        Website = string.Empty;
    }

    public UpdateCompanyCommand(int id, string companyName, int companyTypeId, string address, string phone, string email, string website)
    {
        Id = id;
        CompanyName = companyName;
        CompanyTypeId = companyTypeId;
        Address = address;
        Phone = phone;
        Email = email;
        Website = website;
    }

    internal sealed class UpdateCompanyCommandHandler(
        ICompanyService _companyService,
        IMapper mapper,
        CompanyBusinessRules companyBusinessRules) : IRequestHandler<UpdateCompanyCommand, Result<UpdatedCompanyResponse>>
    {
        public async Task<Result<UpdatedCompanyResponse>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            await companyBusinessRules.CompanyIdShouldBeExist(request.Id);

            Company company = await _companyService.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

            if (company is null)
                throw new BusinessException("Company does not exist.");

            mapper.Map(request, company);

            Company updatedCompany = await _companyService.UpdateAsync(company);

            UpdatedCompanyResponse response = mapper.Map<UpdatedCompanyResponse>(updatedCompany);

            return Result<UpdatedCompanyResponse>.Succeed(response);
        }
    }
}
