using Core.Persistence.Repositories;

namespace QrMenu.Domain.Entities;

public class Company : Entity<int>
{
    public string CompanyName { get; set; }
    public int CompanyTypeId { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public virtual CompanyType CompanyType { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
    public virtual ICollection<Menu> Menus { get; set; }

    public Company()
    {
        Id = 0;
        CompanyName = string.Empty;
        CompanyTypeId = 0;
        Address = string.Empty;
        Phone = string.Empty;
        Email = string.Empty;
        Website = string.Empty;
    }

    public Company(int id, string companyName, int companyTypeId, string address, string phone, string email, string website)
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
