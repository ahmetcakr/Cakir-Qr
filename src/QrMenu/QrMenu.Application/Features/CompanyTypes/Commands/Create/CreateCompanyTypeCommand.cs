using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using QrMenu.Application.Features.CompanyTypes.Constants;
using QrMenu.Application.Features.CompanyTypes.Rules;
using QrMenu.Application.Services.CompaniesService;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Features.CompanyTypes.Commands.Create;

public class CreateCompanyTypeCommand : IRequest<Result<CreatedCompanyTypeResponse>>, ISecuredRequest, ICacheRemoverRequest
{
    public string TypeName { get; set; }
    public string Description { get; set; }


    public string[] Roles => new [] 
    {
        CompanyTypesOperationClaims.Admin,
        CompanyTypesOperationClaims.Write,
        CompanyTypesOperationClaims.Add
    };

    public bool BypassCache => false;
    public string? CacheKey => "";
    public string? CacheGroupKey => "GetCompanyTypes";

    public CreateCompanyTypeCommand()
    {
        TypeName = string.Empty;
        Description = string.Empty;
    }

    public CreateCompanyTypeCommand(string typeName, string description)
    {
        TypeName = typeName;
        Description = description;
    }

    internal sealed class CreateCompanyTypeCommandHandler
        (ICompanyTypeService _companyTypeService,
        IMapper mapper,
        CompanyTypeBusinessRules companyTypeBusinessRules) : IRequestHandler<CreateCompanyTypeCommand, Result<CreatedCompanyTypeResponse>>
    {
        public async Task<Result<CreatedCompanyTypeResponse>> Handle(CreateCompanyTypeCommand request, CancellationToken cancellationToken)
        {
            CompanyType companyType = mapper.Map<CompanyType>(request);

            CompanyType createdCompanyType = await _companyTypeService.AddAsync(companyType);

            CreatedCompanyTypeResponse response = mapper.Map<CreatedCompanyTypeResponse>(createdCompanyType);

            return Result<CreatedCompanyTypeResponse>.Succeed(response,StatusCodes.Status201Created);
        }
    }
}
