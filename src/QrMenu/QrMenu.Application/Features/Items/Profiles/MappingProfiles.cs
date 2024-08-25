
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using QrMenu.Application.Features.Items.Commands.Create;
using QrMenu.Application.Features.Items.Commands.Delete;
using QrMenu.Application.Features.Items.Commands.Update;
using QrMenu.Application.Features.Items.Queries.GetById;
using QrMenu.Application.Features.Items.Queries.GetList;
using QrMenu.Application.Features.Items.Queries.GetListByCategoryId;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Features.Items.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Item, CreatedItemResponse>().ReverseMap();
        CreateMap<Item, CreateItemCommand>().ReverseMap();

        CreateMap<Item, UpdatedItemResponse>().ReverseMap();
        CreateMap<Item, UpdateItemCommand>().ReverseMap();

        CreateMap<Item, DeletedItemResponse>().ReverseMap();
        CreateMap<Item, DeleteItemCommand>().ReverseMap();

        CreateMap<Item, GetByIdItemResponse>().ReverseMap();
        CreateMap<Item, GetByIdItemQuery>().ReverseMap();

        CreateMap<Item, GetListItemResponse>().ReverseMap();
        CreateMap<Item, GetListItemQuery>().ReverseMap();

        CreateMap<Item, GetListByCategoryIdItemResponse>().ReverseMap();
        //CreateMap<Item, GetListByCategoryIdItemQuery>().ReverseMap();

        //CreateMap<List<Item>, List<GetListByCategoryIdItemResponse>>().ReverseMap();

        CreateMap<IPaginate<Item>, GetListResponse<GetListItemResponse>>().ReverseMap();
    }
}
