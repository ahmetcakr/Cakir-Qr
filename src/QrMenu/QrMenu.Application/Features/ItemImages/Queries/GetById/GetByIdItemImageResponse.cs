
using Core.Application.Responses;

namespace QrMenu.Application.Features.ItemImages.Queries.GetById;

public class GetByIdItemImageResponse : IResponse
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public Byte[] Image { get; set; }
    public string Description { get; set; }

    public GetByIdItemImageResponse()
    {
        Id = 0;
        ItemId = 0;
        Image = new Byte[0];
        Description = string.Empty;
    }

    public GetByIdItemImageResponse(int id, int itemId, Byte[] image, string description)
    {
        Id = id;
        this.ItemId = itemId;
        this.Image = image;
        this.Description = description;
    }
}
