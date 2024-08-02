﻿using AutoMapper;
using Core.Application.Pipelines.Authorization;
using QrMenu.Application.Features.CompanyTypes.Constants;
using QrMenu.Application.Features.CompanyTypes.Rules;
using QrMenu.Application.Repositories;
using QrMenu.Domain.Entities;
using MediatR;
using Core.Application.Results;
using Microsoft.AspNetCore.Http;

namespace QrMenu.Application.Features.CompanyTypes.Commands.Create;

public class CreateCompanyTypeCommand : IRequest<Result<CreatedCompanyTypeResponse>>, ISecuredRequest
{
    public string TypeName { get; set; }
    public string Description { get; set; }


    public string[] Roles => new [] 
    {
        CompanyTypesOperationClaims.Admin,
        CompanyTypesOperationClaims.Write,
        CompanyTypesOperationClaims.Add
    };

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
        (ICompanyTypeRepository companyTypeRepository,
        IMapper mapper,
        CompanyTypeBusinessRules companyTypeBusinessRules) : IRequestHandler<CreateCompanyTypeCommand, Result<CreatedCompanyTypeResponse>>
    {
        public async Task<Result<CreatedCompanyTypeResponse>> Handle(CreateCompanyTypeCommand request, CancellationToken cancellationToken)
        {
            CompanyType companyType = mapper.Map<CompanyType>(request);

            CompanyType createdCompanyType = await companyTypeRepository.AddAsync(companyType);

            CreatedCompanyTypeResponse response = mapper.Map<CreatedCompanyTypeResponse>(createdCompanyType);

            return Result<CreatedCompanyTypeResponse>.Succeed(response,StatusCodes.Status201Created);
        }
    }
}
