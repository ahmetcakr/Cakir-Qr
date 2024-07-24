using Core.Application.Responses;

namespace QrMenu.Application.Features.Companies.Queries.GetList;

public class GetListCompanyResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Website { get; set; }

    public GetListCompanyResponse()
    {
        Name = string.Empty;
        Address = string.Empty;
        City = string.Empty;
        Country = string.Empty;
        Email = string.Empty;
        PhoneNumber = string.Empty;
        Website = string.Empty;
    }

    public GetListCompanyResponse(int id, string name, string address, string city, string country, string email, string phoneNumber, string website)
    {
        Id = id;
        Name = name;
        Address = address;
        City = city;
        Country = country;
        Email = email;
        PhoneNumber = phoneNumber;
        Website = website;
    }
}
