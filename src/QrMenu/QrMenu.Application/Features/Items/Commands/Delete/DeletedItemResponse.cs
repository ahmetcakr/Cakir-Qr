
using Core.Application.Responses;

namespace QrMenu.Application.Features.Items.Commands.Delete;
public class DeletedItemResponse : IResponse
{
    public int Id { get; set; }

    public DeletedItemResponse()
    {
        Id = 0;
    }

    public DeletedItemResponse(int id)
    {
        Id = id;
    }
}
