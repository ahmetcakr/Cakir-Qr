using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using QrMenu.Application.Features.Companies.Commands.Create;
using QrMenu.Application.Features.Companies.Commands.Delete;
using QrMenu.Application.Features.Companies.Commands.Update;
using QrMenu.Application.Features.Companies.Queries.GetById;
using QrMenu.Application.Features.Companies.Queries.GetList;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Features.Companies.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Company, CreatedCompanyResponse>().ReverseMap();
        CreateMap<Company, CreateCompanyCommand>().ReverseMap();

        CreateMap<Company, UpdatedCompanyResponse>().ReverseMap();
        CreateMap<Company, UpdateCompanyCommand>().ReverseMap();

        CreateMap<Company, DeletedCompanyResponse>().ReverseMap();
        CreateMap<Company, DeleteCompanyCommand>().ReverseMap();

        CreateMap<Company, GetByIdCompanyResponse>().ReverseMap();
        CreateMap<Company, GetByIdCompanyQuery>().ReverseMap();

        CreateMap<Company, GetListCompanyResponse>().ReverseMap();
        CreateMap<Company, GetListCompanyQuery>().ReverseMap();

        CreateMap<IPaginate<Company>, GetListResponse<GetListCompanyResponse>>().ReverseMap();
    }
}
