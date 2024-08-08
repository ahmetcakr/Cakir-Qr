
using Core.Persistence.Repositories;
using QrMenu.Application.Repositories;
using QrMenu.Domain.Entities;
using QrMenu.Persistence.Contexts;

namespace QrMenu.Persistence.Repositories;

public class MenuRepository : EfRepositoryBase<Menu,
    int, BaseDbContext>, IMenuRepository
{
    public MenuRepository(BaseDbContext context) 
        : base(context) { }
}
