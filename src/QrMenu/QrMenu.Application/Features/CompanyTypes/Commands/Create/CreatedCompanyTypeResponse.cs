using Core.Application.Responses;

namespace QrMenu.Application.Features.CompanyTypes.Commands.Create;

public class CreatedCompanyTypeResponse : IResponse
{
    public int Id { get; set; }
    public string TypeName { get; set; }
    public string Description { get; set; }

    public CreatedCompanyTypeResponse()
    {
        Id = 0;
        TypeName = string.Empty;
        Description = string.Empty;
    }

    public CreatedCompanyTypeResponse(int id, string typeName, string description)
    {
        Id = id;
        TypeName = typeName;
        Description = description;
    }
}
