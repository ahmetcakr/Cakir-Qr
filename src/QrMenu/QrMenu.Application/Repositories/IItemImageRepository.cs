
using Core.Persistence.Repositories;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Repositories;
    public interface IItemImageRepository :  
        IAsyncRepository<ItemImage, int>, IRepository<ItemImage, int> { }
