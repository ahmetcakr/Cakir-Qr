using Core.Application.Responses;

namespace QrMenu.Application.Features.Companies.Queries.GetById;

public class GetByIdCompanyResponse : IResponse
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public int CompanyTypeId { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }

    public GetByIdCompanyResponse()
    {
        Id = 0;
        CompanyName = string.Empty;
        CompanyTypeId = 0;
        Address = string.Empty;
        Phone = string.Empty;
        Email = string.Empty;
        Website = string.Empty;
    }

    public GetByIdCompanyResponse(int id, string companyName, int companyTypeId, string address, string phone, string email, string website)
    {
        Id = id;
        CompanyName = companyName;
        CompanyTypeId = companyTypeId;
        Address = address;
        Phone = phone;
        Email = email;
        Website = website;
    }
}
