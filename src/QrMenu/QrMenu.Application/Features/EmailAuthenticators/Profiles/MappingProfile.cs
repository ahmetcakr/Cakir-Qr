using AutoMapper;
using Core.Security.Entities;
using QrMenu.Application.Features.EmailAuthenticators.Commands.Create;
using QrMenu.Application.Features.EmailAuthenticators.Commands.Update;

namespace QrMenu.Application.Features.EmailAuthenticators.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateEmailAuthenticatorCommand, EmailAuthenticator>()
            .ReverseMap();

        CreateMap<UpdateEmailAuthenticatorCommand, EmailAuthenticator>()
            .ReverseMap();
    }
}
