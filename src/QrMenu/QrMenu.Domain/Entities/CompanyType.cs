using Core.Persistence.Repositories;

namespace QrMenu.Domain.Entities;

public class CompanyType : Entity<int>
{
    public string TypeName { get; set; }
    public string Description { get; set; }

    public virtual ICollection<Company> Companies { get; set; }

    public CompanyType()
    {
        Id = 0;
        TypeName = string.Empty;
    }

    public CompanyType(int id, string typeName, string description)
    {
        Id = id;
        TypeName = typeName;
        Description = description;
    }
}
