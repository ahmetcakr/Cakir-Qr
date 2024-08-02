using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.CrossCuttingConcerns.Exceptions.Types;
using QrMenu.Application.Features.CompanyTypes.Constants;
using QrMenu.Application.Features.CompanyTypes.Rules;
using QrMenu.Application.Repositories;
using QrMenu.Domain.Entities;
using MediatR;
using static QrMenu.Application.Features.CompanyTypes.Constants.CompanyTypesOperationClaims;
using Core.Application.Results;

namespace QrMenu.Application.Features.CompanyTypes.Commands.Delete;

public class DeleteCompanyTypeCommand : IRequest<Result<DeletedCompanyTypeResponse>>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
    {
        Admin,
        CompanyTypesOperationClaims.Delete,
        Write
    };

    public class DeleteCompanyCommandHandler(
        ICompanyTypeRepository companyTypeRepository,
        CompanyTypeBusinessRules companyTypeBusinessRules,
        IMapper mapper) : IRequestHandler<DeleteCompanyTypeCommand, Result<DeletedCompanyTypeResponse>>
    {
        public async Task<Result<DeletedCompanyTypeResponse>> Handle(DeleteCompanyTypeCommand request, CancellationToken cancellationToken)
        {
            await companyTypeBusinessRules.CompanyTypeIdShouldBeExist(request.Id);

            CompanyType companyType = await companyTypeRepository.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

            if (companyType is null)
                throw new BusinessException("CompanyType does not exist.");

            await companyTypeRepository.DeleteAsync(companyType);

            DeletedCompanyTypeResponse response = mapper.Map<DeletedCompanyTypeResponse>(companyType);

            return Result<DeletedCompanyTypeResponse>.Succeed(response);
        }
    }
}
