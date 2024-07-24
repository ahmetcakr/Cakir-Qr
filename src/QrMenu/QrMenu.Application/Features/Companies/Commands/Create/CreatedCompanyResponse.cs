using Core.Application.Responses;

namespace QrMenu.Application.Features.Companies.Commands.Create;

public class CreatedCompanyResponse : IResponse
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public int CompanyTypeId { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }

    public CreatedCompanyResponse()
    {
        Id = 0;
        CompanyName = string.Empty;
        CompanyTypeId = 0;
        Address = string.Empty;
        Phone = string.Empty;
        Email = string.Empty;
        Website = string.Empty;
    }

    public CreatedCompanyResponse(int id, string companyName, int companyTypeId, string address, string phone, string email, string website)
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
