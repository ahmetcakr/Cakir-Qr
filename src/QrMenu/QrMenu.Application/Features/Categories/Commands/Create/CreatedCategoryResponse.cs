
using Core.Application.Responses;

namespace QrMenu.Application.Features.Categories.Commands.Create;
public class CreatedCategoryResponse : IResponse
{
    public int CompanyId { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }

    public CreatedCategoryResponse()
    {
        CompanyId = 0;
        CategoryName = string.Empty;
        Description = string.Empty;
    }

    public CreatedCategoryResponse(int companyId, string categoryName, string description)
    {
        CompanyId = companyId;
        CategoryName = categoryName;
        Description = description;
    }
}
