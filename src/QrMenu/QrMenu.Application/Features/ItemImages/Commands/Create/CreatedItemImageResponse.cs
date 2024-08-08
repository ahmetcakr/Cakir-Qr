
using Core.Application.Responses;

namespace QrMenu.Application.Features.ItemImages.Commands.Create;
public class CreatedItemImageResponse : IResponse
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public Byte[] Image { get; set; }
    public string Description { get; set; }

    public CreatedItemImageResponse()
    {
        Id = 0;
        ItemId = 0;
        Image = new Byte[0];
        Description = string.Empty;
    }

    public CreatedItemImageResponse(int id, int itemId, Byte[] image, string description)
    {
        this.Id = id;
        this.ItemId = itemId;
        this.Image = image;
        this.Description = description;
    }
}
