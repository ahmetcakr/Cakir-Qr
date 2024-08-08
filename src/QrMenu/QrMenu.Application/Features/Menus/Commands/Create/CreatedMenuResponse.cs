
using Core.Application.Responses;

namespace QrMenu.Application.Features.Menus.Commands.Create;
public class CreatedMenuResponse : IResponse
{
    public int CompanyId { get; set; }
    public string MenuName { get; set; }
    public string Description { get; set; }

    public CreatedMenuResponse()
    {
        CompanyId = 0;
        MenuName = string.Empty;
        Description = string.Empty;
    }

    public CreatedMenuResponse(int companyId, string menuName, string description)
    {
        this.CompanyId = companyId;
        this.MenuName = menuName;
        this.Description = description;
    }
}
