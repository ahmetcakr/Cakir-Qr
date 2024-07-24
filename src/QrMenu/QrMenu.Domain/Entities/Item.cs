using Core.Persistence.Repositories;

namespace QrMenu.Domain.Entities;

public class Item : Entity<int>
{
    public int CategoryId { get; set; }
    public string ItemName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }

    public virtual Category Category { get; set; }
    public virtual ICollection<ItemImage> ItemImages { get; set; }
    public virtual ItemIngredient ItemIngredient { get; set; }

    public Item()
    {
        Id = 0;
        CategoryId = 0;
        ItemName = string.Empty;
        Description = string.Empty;
        Price = 0;
        IsAvailable = false;
    }

    public Item(int id, int categoryId, string itemName, string description, decimal price, bool isAvailable)
    {
        Id = id;
        CategoryId = categoryId;
        ItemName =itemName;
        Description = description;
        Price = price;
        IsAvailable = isAvailable;
    }
}
