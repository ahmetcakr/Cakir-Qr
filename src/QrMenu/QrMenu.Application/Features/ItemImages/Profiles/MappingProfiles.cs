
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using QrMenu.Application.Features.ItemImages.Commands.Create;
using QrMenu.Application.Features.ItemImages.Commands.Delete;
using QrMenu.Application.Features.ItemImages.Commands.Update;
using QrMenu.Application.Features.ItemImages.Queries.GetById;
using QrMenu.Application.Features.ItemImages.Queries.GetList;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Features.ItemImages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ItemImage, CreatedItemImageResponse>().ReverseMap();
        CreateMap<ItemImage, CreateItemImageCommand>().ReverseMap();

        CreateMap<ItemImage, UpdatedItemImageResponse>().ReverseMap();
        CreateMap<ItemImage, UpdateItemImageCommand>().ReverseMap();

        CreateMap<ItemImage, DeletedItemImageResponse>().ReverseMap();
        CreateMap<ItemImage, DeleteItemImageCommand>().ReverseMap();

        CreateMap<ItemImage, GetByIdItemImageResponse>().ReverseMap();
        CreateMap<ItemImage, GetByIdItemImageQuery>().ReverseMap();

        CreateMap<ItemImage, GetListItemImageResponse>().ReverseMap();
        CreateMap<ItemImage, GetListItemImageQuery>().ReverseMap();

        CreateMap<IPaginate<ItemImage>, GetListResponse<GetListItemImageResponse>>().ReverseMap();
    }
}
