
using Core.Application.Responses;

namespace QrMenu.Application.Features.Menus.Commands.Update;

public class UpdatedMenuResponse : IResponse
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string MenuName { get; set; }
    public string Description { get; set; }

    public UpdatedMenuResponse()
    {
        Id = 0;
        CompanyId = 0;
        MenuName = string.Empty;
        Description = string.Empty;
    }

    public UpdatedMenuResponse(int id, int companyId, string menuName, string description)
    {
        this.Id = id;
        this.CompanyId = companyId;
        this.MenuName = menuName;
        this.Description = description;
    }
}
