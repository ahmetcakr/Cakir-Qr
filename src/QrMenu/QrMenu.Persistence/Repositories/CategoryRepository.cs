
using Core.Persistence.Repositories;
using QrMenu.Application.Repositories;
using QrMenu.Domain.Entities;
using QrMenu.Persistence.Contexts;

namespace QrMenu.Persistence.Repositories;

public class CategoryRepository : EfRepositoryBase<Category,
    int, BaseDbContext>, ICategoryRepository
{
    public CategoryRepository(BaseDbContext context) 
        : base(context) { }
}
