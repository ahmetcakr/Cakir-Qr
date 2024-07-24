using Core.Application.Responses;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace QrMenu.Application.Features.CompanyTypes.Commands.Update;

public class UpdatedCompanyTypeResponse : IResponse
{
    public int Id { get; set; }
    public string TypeName { get; set; }
    public string Description { get; set; }

    public UpdatedCompanyTypeResponse()
    {
        Id = 0;
        TypeName = string.Empty;
        Description = string.Empty;
    }

    public UpdatedCompanyTypeResponse(int id, string typeName, string description)
    {
        Id = id;
        TypeName = typeName;
        Description = description;
    }

}
