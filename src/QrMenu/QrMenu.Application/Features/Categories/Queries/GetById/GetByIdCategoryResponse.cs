
using Core.Application.Responses;

namespace QrMenu.Application.Features.Categories.Queries.GetById;

public class GetByIdCategoryResponse : IResponse
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
public string CategoryName { get; set; }
public string Description { get; set; }

    public GetByIdCategoryResponse()
    {
        // Varsayılan değerler burada atanabilir
    }

    public GetByIdCategoryResponse(int CompanyId, string CategoryName, string Description)
    {
        // Özelliklerin değerlerini burada atayın
    }
}

