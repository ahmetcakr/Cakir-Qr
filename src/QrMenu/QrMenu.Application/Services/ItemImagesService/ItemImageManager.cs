
using Core.Persistence.Paging;
using QrMenu.Application.Features.ItemImages.Rules;
using QrMenu.Application.Repositories;
using QrMenu.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;


namespace QrMenu.Application.Services.ItemImagesService;
    public class ItemImageManager(
        IItemImageRepository itemimageRepository,
        ItemImageBusinessRules itemimageBusinessRules) : IItemImageService
{
    public async Task<ItemImage> AddAsync(ItemImage itemimage)
    {
         // await itemimageBusinessRules;
         // you can add your business rules here
    
        return await itemimageRepository.AddAsync(itemimage);
    }

    public async Task<ItemImage> DeleteAsync(ItemImage itemimage, bool permanent = false)
    {
        return await itemimageRepository.DeleteAsync(itemimage, permanent);
    }

    public async Task<ItemImage?> GetAsync(Expression<Func<ItemImage, bool>> predicate, Func<IQueryable<ItemImage>, IIncludableQueryable<ItemImage, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
         return await itemimageRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
    }

    public async Task<IPaginate<ItemImage?>?> GetListAsync(Expression<Func<ItemImage, bool>>? predicate = null, Func<IQueryable<ItemImage>, IOrderedQueryable<ItemImage>>? orderBy = null, Func<IQueryable<ItemImage>, IIncludableQueryable<ItemImage, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IPaginate<ItemImage> response = await itemimageRepository.GetListAsync(
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

    public async Task<ItemImage> UpdateAsync(ItemImage request)
    {
         return await itemimageRepository.UpdateAsync(request);
    }


}
