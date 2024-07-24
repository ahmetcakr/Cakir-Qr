using Core.Persistence.Repositories;

namespace QrMenu.Domain.Entities;

public class ItemIngredient : Entity<int>
{
    public int ItemId { get; set; }
    public virtual Item? Item { get; set; }

    public ItemIngredient()
    {
        Id = 0;
        ItemId = 0;
    }

    public ItemIngredient(int id, int itemId)
    {
        Id = id;
        ItemId = itemId;
    }
}
