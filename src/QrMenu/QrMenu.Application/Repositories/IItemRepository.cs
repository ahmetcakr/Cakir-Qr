
using Core.Persistence.Repositories;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Repositories;
    public interface IItemRepository :  
        IAsyncRepository<Item, int>, IRepository<Item, int> { }
