﻿using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;
using QrMenu.Application.Features.OperationClaims.Commands.Create;
using QrMenu.Application.Features.OperationClaims.Commands.Delete;
using QrMenu.Application.Features.OperationClaims.Commands.Update;
using QrMenu.Application.Features.OperationClaims.Queries.GetById;
using QrMenu.Application.Features.OperationClaims.Queries.GetList;

namespace QrMenu.Application.Features.OperationClaims.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<OperationClaim, CreateOperationClaimCommand>().ReverseMap();
        CreateMap<OperationClaim, CreatedOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim, UpdateOperationClaimCommand>().ReverseMap();
        CreateMap<OperationClaim, UpdatedOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim, DeleteOperationClaimCommand>().ReverseMap();
        CreateMap<OperationClaim, DeletedOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim, GetByIdOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim, GetListOperationClaimListItemDto>().ReverseMap();
        CreateMap<IPaginate<OperationClaim>, GetListResponse<GetListOperationClaimListItemDto>>().ReverseMap();
    }
}

