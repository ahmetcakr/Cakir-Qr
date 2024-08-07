
using Core.Application.Responses;

namespace QrMenu.Application.Features.Categories.Queries.GetList;

public class GetListCategoryResponse : IResponse
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }

    public GetListCategoryResponse()
    {
        Id = 0;
        CompanyId = 0;
        CategoryName = string.Empty;
        Description = string.Empty;
    }

    public GetListCategoryResponse(int CompanyId, string CategoryName, string Description)
    {
        this.CompanyId = CompanyId;
        this.CategoryName = CategoryName;
        this.Description = Description;
        
    }
}

