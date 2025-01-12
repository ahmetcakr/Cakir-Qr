
using Core.Application.Responses;

namespace QrMenu.Application.Features.Categories.Commands.Create;
public class CreatedCategoryResponse : IResponse
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }

    public CreatedCategoryResponse()
    {
        Id = 0;
        CompanyId = 0;
        CategoryName = string.Empty;
        Description = string.Empty;
    }

    public CreatedCategoryResponse(int id, int companyId, string categoryName, string description)
    {
        Id = id;
        CompanyId = companyId;
        CategoryName = categoryName;
        Description = description;
    }
}
