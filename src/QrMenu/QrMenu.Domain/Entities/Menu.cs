using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrMenu.Domain.Entities;

public class Menu : Entity<int>
{
    public int CompanyId { get; set; }
    public string MenuName { get; set; }
    public string Description { get; set; } 
    public virtual Company Company { get; set; }
    public virtual MenuQrCode MenuQrCode { get; set; }

    public Menu()
    {
        Id = 0;
        CompanyId = 0;
        MenuName = string.Empty;
        Description = string.Empty;
    }

    public Menu(int id, int companyId, string menuName, string description)
    {
        Id = id;
        CompanyId = companyId;
        MenuName = menuName;
        Description = description;
    }
}
