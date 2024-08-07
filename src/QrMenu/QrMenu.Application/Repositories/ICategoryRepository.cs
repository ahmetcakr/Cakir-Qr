
using Core.Persistence.Repositories;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Repositories;
    public interface ICategoryRepository :  
        IAsyncRepository<Category, int>, IRepository<Category, int> { }
