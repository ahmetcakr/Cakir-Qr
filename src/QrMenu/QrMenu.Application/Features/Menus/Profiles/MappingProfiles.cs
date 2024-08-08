
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using QrMenu.Application.Features.Menus.Commands.Create;
using QrMenu.Application.Features.Menus.Commands.Delete;
using QrMenu.Application.Features.Menus.Commands.Update;
using QrMenu.Application.Features.Menus.Queries.GetById;
using QrMenu.Application.Features.Menus.Queries.GetList;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Features.Menus.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Menu, CreatedMenuResponse>().ReverseMap();
        CreateMap<Menu, CreateMenuCommand>().ReverseMap();

        CreateMap<Menu, UpdatedMenuResponse>().ReverseMap();
        CreateMap<Menu, UpdateMenuCommand>().ReverseMap();

        CreateMap<Menu, DeletedMenuResponse>().ReverseMap();
        CreateMap<Menu, DeleteMenuCommand>().ReverseMap();

        CreateMap<Menu, GetByIdMenuResponse>().ReverseMap();
        CreateMap<Menu, GetByIdMenuQuery>().ReverseMap();

        CreateMap<Menu, GetListMenuResponse>().ReverseMap();
        CreateMap<Menu, GetListMenuQuery>().ReverseMap();

        CreateMap<IPaginate<Menu>, GetListResponse<GetListMenuResponse>>().ReverseMap();
    }
}
