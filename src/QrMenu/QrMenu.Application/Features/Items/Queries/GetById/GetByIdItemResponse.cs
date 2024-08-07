
using Core.Application.Responses;

namespace QrMenu.Application.Features.Items.Queries.GetById;

public class GetByIdItemResponse : IResponse
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string ItemName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }

    public GetByIdItemResponse()
    {
        Id = 0;
        CategoryId = 0;
        ItemName = string.Empty;
        Description = string.Empty;
        Price = default;
        IsAvailable = default;
    }

    public GetByIdItemResponse(int id, int categoryId, string ıtemName, string description, decimal price, bool ısAvailable)
    {
        Id = id;
        this.CategoryId = categoryId;
        this.ItemName = ıtemName;
        this.Description = description;
        this.Price = price;
        this.IsAvailable = ısAvailable;
    }
}
