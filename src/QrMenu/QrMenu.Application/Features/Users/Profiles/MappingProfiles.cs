using QrMenu.Application.Features.Users.Commands.Create;
using QrMenu.Application.Features.Users.Commands.Delete;
using QrMenu.Application.Features.Users.Commands.Update;
using QrMenu.Application.Features.Users.Commands.UpdateFromAuth;
using QrMenu.Application.Features.Users.Queries.GetById;
using QrMenu.Application.Features.Users.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace QrMenu.Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, CreateUserCommand>().ReverseMap();
        CreateMap<User, CreatedUserResponse>().ReverseMap();
        CreateMap<User, UpdateUserCommand>().ReverseMap();
        CreateMap<User, UpdatedUserResponse>().ReverseMap();
        CreateMap<User, UpdateUserFromAuthCommand>().ReverseMap();
        CreateMap<User, UpdatedUserFromAuthResponse>().ReverseMap();
        CreateMap<User, DeleteUserCommand>().ReverseMap();
        CreateMap<User, DeletedUserResponse>().ReverseMap();
        CreateMap<User, GetByIdUserResponse>().ReverseMap();
        CreateMap<User, GetListUserListItemDto>().ReverseMap();
        CreateMap<IPaginate<User>, GetListResponse<GetListUserListItemDto>>().ReverseMap();
    }
}
