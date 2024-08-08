
using Core.Application.Responses;

namespace QrMenu.Application.Features.Menus.Commands.Delete;
public class DeletedMenuResponse : IResponse
{
    public int Id { get; set; }

    public DeletedMenuResponse()
    {
        Id = 0;
    }

    public DeletedMenuResponse(int id)
    {
        Id = id;
    }
}
