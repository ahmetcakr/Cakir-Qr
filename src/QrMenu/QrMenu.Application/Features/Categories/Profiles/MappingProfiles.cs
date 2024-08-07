
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using QrMenu.Application.Features.Categories.Commands.Create;
using QrMenu.Application.Features.Categories.Commands.Delete;
using QrMenu.Application.Features.Categories.Commands.Update;
using QrMenu.Application.Features.Categories.Queries.GetById;
using QrMenu.Application.Features.Categories.Queries.GetList;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Features.Categories.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Category, CreatedCategoryResponse>().ReverseMap();
        CreateMap<Category, CreateCategoryCommand>().ReverseMap();

        CreateMap<Category, UpdatedCategoryResponse>().ReverseMap();
        CreateMap<Category, UpdateCategoryCommand>().ReverseMap();

        CreateMap<Category, DeletedCategoryResponse>().ReverseMap();
        CreateMap<Category, DeleteCategoryCommand>().ReverseMap();

        CreateMap<Category, GetByIdCategoryResponse>().ReverseMap();
        CreateMap<Category, GetByIdCategoryQuery>().ReverseMap();

        CreateMap<Category, GetListCategoryResponse>().ReverseMap();
        CreateMap<Category, GetListCategoryQuery>().ReverseMap();

        CreateMap<Category, CreatedCategoryResponse>().ReverseMap();
        CreateMap<Category, CreateCategoryCommand>().ReverseMap();


        CreateMap<CreatedCategoryResponse, Category>().ReverseMap();
        CreateMap<CreateCategoryCommand, Category>().ReverseMap();


        CreateMap<IPaginate<Category>, GetListResponse<GetListCategoryResponse>>().ReverseMap();
    }
}
