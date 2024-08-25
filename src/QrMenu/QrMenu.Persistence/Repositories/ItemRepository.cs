
using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using QrMenu.Application.Repositories;
using QrMenu.Domain.Entities;
using QrMenu.Persistence.Contexts;

namespace QrMenu.Persistence.Repositories;

public class ItemRepository : EfRepositoryBase<Item,
    int, BaseDbContext>, IItemRepository
{
    private readonly DbSet<Item> _context;

    public ItemRepository(BaseDbContext context) : base(context) {
    
        _context = context.Set<Item>();
    }

    public async Task<List<Item>> GetListItemsByCategoryId(int categoryId)
    {
        return await _context
            .Where(x => x.CategoryId == categoryId)  // Where koþulunu ekleyin
            .AsNoTracking()
            .ToListAsync();
    }
}
