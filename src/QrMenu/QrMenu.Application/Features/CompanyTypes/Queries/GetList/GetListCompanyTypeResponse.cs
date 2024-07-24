using Core.Application.Responses;

namespace QrMenu.Application.Features.CompanyTypes.Queries.GetList;

public class GetListCompanyTypeResponse : IResponse
{
    public int Id { get; set; }
    public string TypeName { get; set; }
    public string Description { get; set; }

    public GetListCompanyTypeResponse()
    {
        Id = 0;
        TypeName = string.Empty;
        Description = string.Empty;
    }

    public GetListCompanyTypeResponse(int id, string typeName, string description)
    {
        Id = id;
        TypeName = typeName;
        Description = description;
    }
}
