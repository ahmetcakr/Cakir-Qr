﻿using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using QrMenu.Application.Features.OperationClaims.Rules;
using QrMenu.Application.Repositories;
using MediatR;
using static QrMenu.Application.Features.OperationClaims.Constants.OperationClaimsOperationClaims;
using Core.Application.Results;
using Microsoft.AspNetCore.Http;

namespace QrMenu.Application.Features.OperationClaims.Commands.Create;

public class CreateOperationClaimCommand : IRequest<Result<CreatedOperationClaimResponse>>, ISecuredRequest
{
    public string Name { get; set; }

    public CreateOperationClaimCommand()
    {
        Name = string.Empty;
    }

    public CreateOperationClaimCommand(string name)
    {
        Name = name;
    }

    public string[] Roles => new[] { Admin, Write, Add };

    public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, Result<CreatedOperationClaimResponse>>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public CreateOperationClaimCommandHandler(
            IOperationClaimRepository operationClaimRepository,
            IMapper mapper,
            OperationClaimBusinessRules operationClaimBusinessRules
        )
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<Result<CreatedOperationClaimResponse>> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
        {
            await _operationClaimBusinessRules.OperationClaimNameShouldNotExistWhenCreating(request.Name);
            OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);

            OperationClaim createdOperationClaim = await _operationClaimRepository.AddAsync(mappedOperationClaim);

            CreatedOperationClaimResponse response = _mapper.Map<CreatedOperationClaimResponse>(createdOperationClaim);
            return Result<CreatedOperationClaimResponse>.Succeed(response, StatusCodes.Status201Created);
        }
    }
}
