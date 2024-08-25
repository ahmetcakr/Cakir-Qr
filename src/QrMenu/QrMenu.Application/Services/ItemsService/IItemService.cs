
using Core.Persistence.Services;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Services.ItemsService;
public interface IItemService : IService<Item> {

    Task<List<Item>> GetListByCategoryIdAsync(int categoryId);

}
