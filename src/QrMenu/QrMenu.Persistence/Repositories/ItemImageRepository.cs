
using Core.Persistence.Repositories;
using QrMenu.Application.Repositories;
using QrMenu.Domain.Entities;
using QrMenu.Persistence.Contexts;

namespace QrMenu.Persistence.Repositories;

public class ItemImageRepository : EfRepositoryBase<ItemImage,
    int, BaseDbContext>, IItemImageRepository
{
    public ItemImageRepository(BaseDbContext context) 
        : base(context) { }
}
