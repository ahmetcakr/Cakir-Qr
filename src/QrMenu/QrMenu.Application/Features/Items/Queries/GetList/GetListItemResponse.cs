
using Core.Application.Responses;

namespace QrMenu.Application.Features.Items.Queries.GetList;

public class GetListItemResponse : IResponse
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string ItemName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }

    public GetListItemResponse()
    {
        Id = 0;
        CategoryId = 0;
        ItemName = string.Empty;
        Description = string.Empty;
        Price = default;
        IsAvailable = default;
    }

    public GetListItemResponse(int id, int categoryId, string itemName, string description, decimal price, bool isAvailable)
    {
        this.Id = id;
        this.CategoryId = categoryId;
        this.ItemName = itemName;
        this.Description = description;
        this.Price = price;
        this.IsAvailable = isAvailable;
    }
}

