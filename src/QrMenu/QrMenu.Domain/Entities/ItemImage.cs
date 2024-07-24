using Core.Persistence.Repositories;

namespace QrMenu.Domain.Entities;

public class ItemImage : Entity<int>
{
    public int ItemId { get; set; }
    public Byte[] Image { get; set; }
    public string Description { get; set; }
    public virtual Item Item { get; set; }

    public ItemImage()
    {
        Id = 0;
        ItemId = 0;
        Image = new Byte[0];
        Description = string.Empty;
    }

    public ItemImage(int id, int itemId, Byte[] image, string description)
    {
        Id = id;
        ItemId = itemId;
        Image = image;
        Description = description;
    }
}
