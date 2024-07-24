using Core.Persistence.Repositories;

namespace QrMenu.Domain.Entities;

public class Category : Entity<int>
{
    public int CompanyId { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }

    public virtual Company Company { get; set; }
    public virtual ICollection<Item> Items { get; set; }

    public Category()
    {
        Id = 0;
        CompanyId = 0;
        CategoryName = string.Empty;
        Description = string.Empty;
    }

    public Category(int id, int companyId, string categoryName, string description)
    {
        Id = id;
        CompanyId = companyId;
        CategoryName = categoryName;
        Description = description;
    }
}
