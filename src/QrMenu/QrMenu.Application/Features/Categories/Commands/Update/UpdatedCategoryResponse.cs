
using Core.Application.Responses;

namespace QrMenu.Application.Features.Categories.Commands.Update;

public class UpdatedCategoryResponse : IResponse
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
public string CategoryName { get; set; }
public string Description { get; set; }

    public UpdatedCategoryResponse()
    {
        // Varsayılan değerler burada atanabilir
    }

    public UpdatedCategoryResponse(int CompanyId, string CategoryName, string Description)
    {
        // Özelliklerin değerlerini burada atayın
    }
}

