using Core.Application.Responses;

namespace QrMenu.Application.Features.CompanyTypes.Queries.GetById;

public class GetByIdCompanyTypeResponse : IResponse
{
    public int Id { get; set; }
    public string TypeName { get; set; }
    public string Description { get; set; }

    public GetByIdCompanyTypeResponse()
    {
        Id = 0;
        TypeName = string.Empty;
        Description = string.Empty;
    }

    public GetByIdCompanyTypeResponse(int id, string typeName, string description)
    {
        Id = id;
        TypeName = typeName;
        Description = description;
    }
}
