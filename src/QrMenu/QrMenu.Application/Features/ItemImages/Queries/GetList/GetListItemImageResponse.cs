
using Core.Application.Responses;

namespace QrMenu.Application.Features.ItemImages.Queries.GetList;

public class GetListItemImageResponse : IResponse
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public Byte[] Image { get; set; }
    public string Description { get; set; }

    public GetListItemImageResponse()
    {
        Id = 0;
        ItemId = 0;
        Image = default;
        Description = string.Empty;
    }

    public GetListItemImageResponse(int id, int itemId, Byte[] image, string description)
    {
        this.Id = id;
        this.ItemId = itemId;
        this.Image = image;
        this.Description = description;
    }
}

