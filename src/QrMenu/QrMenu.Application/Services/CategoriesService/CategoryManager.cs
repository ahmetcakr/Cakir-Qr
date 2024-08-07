
using Core.Persistence.Paging;
using QrMenu.Application.Features.Categories.Rules;
using QrMenu.Application.Repositories;
using QrMenu.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;


namespace QrMenu.Application.Services.CategoriesService;
    public class CategoryManager(
        ICategoryRepository categoryRepository,
        CategoryBusinessRules categoryBusinessRules) : ICategoryService
{
    public async Task<Category> AddAsync(Category category)
    {
         // await categoryBusinessRules;
         // you can add your business rules here
    
        return await categoryRepository.AddAsync(category);
    }

    public async Task<Category> DeleteAsync(Category category, bool permanent = false)
    {
        return await categoryRepository.DeleteAsync(category, permanent);
    }

    public async Task<Category?> GetAsync(Expression<Func<Category, bool>> predicate, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
         return await categoryRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
    }

    public async Task<IPaginate<Category?>?> GetListAsync(Expression<Func<Category, bool>>? predicate = null, Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IPaginate<Category> response = await categoryRepository.GetListAsync(
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

    public async Task<Category> UpdateAsync(Category request)
    {
         return await categoryRepository.UpdateAsync(request);
    }


}
