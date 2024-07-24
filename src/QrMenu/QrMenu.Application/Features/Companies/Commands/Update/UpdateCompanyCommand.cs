using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.CrossCuttingConcerns.Exceptions.Types;
using QrMenu.Application.Features.Companies.Constants;
using QrMenu.Application.Features.Companies.Rules;
using QrMenu.Application.Repositories;
using QrMenu.Domain.Entities;
using MediatR;

namespace QrMenu.Application.Features.Companies.Commands.Update;

public class UpdateCompanyCommand : IRequest<UpdatedCompanyResponse>, ISecuredRequest
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
        ICompanyRepository companyRepository,
        IMapper mapper,
        CompanyBusinessRules companyBusinessRules) : IRequestHandler<UpdateCompanyCommand, UpdatedCompanyResponse>
    {
        public async Task<UpdatedCompanyResponse> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            await companyBusinessRules.CompanyIdShouldBeExist(request.Id);

            Company company = await companyRepository.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

            if (company is null)
                throw new BusinessException("Company does not exist.");

            mapper.Map(request, company);

            Company updatedCompany = await companyRepository.UpdateAsync(company);

            UpdatedCompanyResponse response = mapper.Map<UpdatedCompanyResponse>(updatedCompany);

            return response;
        }
    }
}
