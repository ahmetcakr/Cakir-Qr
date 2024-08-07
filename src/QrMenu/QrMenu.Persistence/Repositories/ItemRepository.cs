
using Core.Persistence.Repositories;
using QrMenu.Application.Repositories;
using QrMenu.Domain.Entities;
using QrMenu.Persistence.Contexts;

namespace QrMenu.Persistence.Repositories;

public class ItemRepository : EfRepositoryBase<Item,
    int, BaseDbContext>, IItemRepository
{
    public ItemRepository(BaseDbContext context) 
        : base(context) { }
}
