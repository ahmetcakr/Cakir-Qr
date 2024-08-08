using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Results;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;
using QrMenu.Application.Features.CompanyTypes.Constants;
using QrMenu.Application.Features.CompanyTypes.Rules;
using QrMenu.Application.Services.CompaniesService;

namespace QrMenu.Application.Features.CompanyTypes.Commands.Update;

public class UpdateCompanyTypeCommand : IRequest<Result<UpdatedCompanyTypeResponse>>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }
    public string TypeName { get; set; }
    public string Description { get; set; }

    public string[] Roles => new[]
    {
        CompanyTypesOperationClaims.Admin,
        CompanyTypesOperationClaims.Write,
        CompanyTypesOperationClaims.Update
    };

    public bool BypassCache => false;
    public string? CacheKey => "";
    public string? CacheGroupKey => "GetCompanyTypes";

    public UpdateCompanyTypeCommand()
    {
        Id = 0;
        TypeName = string.Empty;
        Description = string.Empty;
    }

    public UpdateCompanyTypeCommand(int id, string typeName, string description)
    {
        Id = id;
        TypeName = typeName;
        Description = description;
    }

    internal sealed class UpdateCompanyTypeCommandHandler(
        ICompanyTypeService _companyTypeService,
        IMapper mapper,
        CompanyTypeBusinessRules companyTypeBusinessRules) : IRequestHandler<UpdateCompanyTypeCommand, Result<UpdatedCompanyTypeResponse>>
    {
        public async Task<Result<UpdatedCompanyTypeResponse>> Handle(UpdateCompanyTypeCommand request, CancellationToken cancellationToken)
        {
            await companyTypeBusinessRules.CompanyTypeIdShouldBeExist(request.Id);

            var companyType = await _companyTypeService.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

            if (companyType is null)
                throw new BusinessException("Company does not exist.");

            mapper.Map(request, companyType);

            var updatedCompanyType = await _companyTypeService.UpdateAsync(companyType);

            UpdatedCompanyTypeResponse response = mapper.Map<UpdatedCompanyTypeResponse>(updatedCompanyType);

            return Result<UpdatedCompanyTypeResponse>.Succeed(response);
        }
    }
}
