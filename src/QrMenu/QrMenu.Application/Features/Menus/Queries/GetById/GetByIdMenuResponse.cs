
using Core.Application.Responses;

namespace QrMenu.Application.Features.Menus.Queries.GetById;

public class GetByIdMenuResponse : IResponse
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string MenuName { get; set; }
    public string Description { get; set; }

    public GetByIdMenuResponse()
    {
        Id = 0;
        CompanyId = 0;
        MenuName = string.Empty;
        Description = string.Empty;
    }

    public GetByIdMenuResponse(int id, int companyId, string menuName, string description)
    {
        Id = id;
        this.CompanyId = companyId;
        this.MenuName = menuName;
        this.Description = description;
    }
}
