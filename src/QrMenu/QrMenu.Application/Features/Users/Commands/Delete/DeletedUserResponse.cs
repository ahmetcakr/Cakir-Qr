using Core.Application.Responses;

namespace QrMenu.Application.Features.Users.Commands.Delete;

public class DeletedUserResponse : IResponse
{
    public int Id { get; set; }
}
