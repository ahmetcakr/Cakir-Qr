using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using QrMenu.Application.Features.CompanyTypes.Commands.Create;
using QrMenu.Application.Features.CompanyTypes.Commands.Delete;
using QrMenu.Application.Features.CompanyTypes.Commands.Update;
using QrMenu.Application.Features.CompanyTypes.Queries.GetById;
using QrMenu.Application.Features.CompanyTypes.Queries.GetList;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Features.CompanyTypes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CompanyType, CreatedCompanyTypeResponse>().ReverseMap();
        CreateMap<CompanyType, CreateCompanyTypeCommand>().ReverseMap();

        CreateMap<CompanyType, UpdatedCompanyTypeResponse>().ReverseMap();
        CreateMap<CompanyType, UpdateCompanyTypeCommand>().ReverseMap();

        CreateMap<CompanyType, DeletedCompanyTypeResponse>().ReverseMap();
        CreateMap<CompanyType, DeleteCompanyTypeCommand>().ReverseMap();

        CreateMap<CompanyType, GetByIdCompanyTypeResponse>().ReverseMap();
        CreateMap<CompanyType, GetByIdCompanyTypeQuery>().ReverseMap();

        CreateMap<CompanyType, GetListCompanyTypeResponse>().ReverseMap();
        CreateMap<CompanyType, GetListCompanyTypeQuery>().ReverseMap();

        CreateMap<IPaginate<CompanyType>, GetListResponse<GetListCompanyTypeResponse>>().ReverseMap();
    }
}
