
using Core.Persistence.Paging;
using QrMenu.Application.Features.Items.Rules;
using QrMenu.Application.Repositories;
using QrMenu.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;


namespace QrMenu.Application.Services.ItemsService;
    public class ItemManager(
        IItemRepository itemRepository,
        ItemBusinessRules itemBusinessRules) : IItemService
{
    public async Task<Item> AddAsync(Item item)
    {
         // await itemBusinessRules;
         // you can add your business rules here
    
        return await itemRepository.AddAsync(item);
    }

    public async Task<Item> DeleteAsync(Item item, bool permanent = false)
    {
        return await itemRepository.DeleteAsync(item, permanent);
    }

    public async Task<Item?> GetAsync(Expression<Func<Item, bool>> predicate, Func<IQueryable<Item>, IIncludableQueryable<Item, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
         return await itemRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
    }

    public async Task<IPaginate<Item?>?> GetListAsync(Expression<Func<Item, bool>>? predicate = null, Func<IQueryable<Item>, IOrderedQueryable<Item>>? orderBy = null, Func<IQueryable<Item>, IIncludableQueryable<Item, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IPaginate<Item> response = await itemRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );

        return response;
    }

    public async Task<Item> UpdateAsync(Item request)
    {
         return await itemRepository.UpdateAsync(request);
    }


}
