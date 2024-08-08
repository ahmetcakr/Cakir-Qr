
using Core.Persistence.Repositories;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Repositories;
    public interface IMenuRepository :  
        IAsyncRepository<Menu, int>, IRepository<Menu, int> { }
