
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using QrMenu.Application.Features.MenuQrCodes.Commands.Create;
using QrMenu.Application.Features.MenuQrCodes.Commands.Delete;
using QrMenu.Application.Features.MenuQrCodes.Commands.Update;
using QrMenu.Application.Features.MenuQrCodes.Queries.GetById;
using QrMenu.Application.Features.MenuQrCodes.Queries.GetList;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Features.MenuQrCodes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<MenuQrCode, CreatedMenuQrCodeResponse>().ReverseMap();
        CreateMap<MenuQrCode, CreateMenuQrCodeCommand>().ReverseMap();

        CreateMap<MenuQrCode, UpdatedMenuQrCodeResponse>().ReverseMap();
        CreateMap<MenuQrCode, UpdateMenuQrCodeCommand>().ReverseMap();

        CreateMap<MenuQrCode, DeletedMenuQrCodeResponse>().ReverseMap();
        CreateMap<MenuQrCode, DeleteMenuQrCodeCommand>().ReverseMap();

        CreateMap<MenuQrCode, GetByIdMenuQrCodeResponse>().ReverseMap();
        CreateMap<MenuQrCode, GetByIdMenuQrCodeQuery>().ReverseMap();

        CreateMap<MenuQrCode, GetListMenuQrCodeResponse>().ReverseMap();
        CreateMap<MenuQrCode, GetListMenuQrCodeQuery>().ReverseMap();

        CreateMap<IPaginate<MenuQrCode>, GetListResponse<GetListMenuQrCodeResponse>>().ReverseMap();
    }
}
