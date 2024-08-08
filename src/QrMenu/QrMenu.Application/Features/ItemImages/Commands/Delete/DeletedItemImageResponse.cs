
using Core.Application.Responses;

namespace QrMenu.Application.Features.ItemImages.Commands.Delete;
public class DeletedItemImageResponse : IResponse
{
    public int Id { get; set; }

    public DeletedItemImageResponse()
    {
        Id = 0;
    }

    public DeletedItemImageResponse(int id)
    {
        Id = id;
    }
}
