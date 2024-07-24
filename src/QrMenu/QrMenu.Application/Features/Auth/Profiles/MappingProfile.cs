using AutoMapper;
using Core.Security.Entities;
using QrMenu.Application.Features.Auth.Commands.RevokeToken;

namespace QrMenu.Application.Features.Auth.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RefreshToken, RevokedTokenResponse>().ReverseMap();
    }
}
