
using Core.Persistence.Paging;
using QrMenu.Application.Features.Menus.Rules;
using QrMenu.Application.Repositories;
using QrMenu.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;


namespace QrMenu.Application.Services.MenusService;
    public class MenuManager(
        IMenuRepository menuRepository,
        MenuBusinessRules menuBusinessRules) : IMenuService
{
    public async Task<Menu> AddAsync(Menu menu)
    {
         // await menuBusinessRules;
         // you can add your business rules here
    
        return await menuRepository.AddAsync(menu);
    }

    public async Task<Menu> DeleteAsync(Menu menu, bool permanent = false)
    {
        return await menuRepository.DeleteAsync(menu, permanent);
    }

    public async Task<Menu?> GetAsync(Expression<Func<Menu, bool>> predicate, Func<IQueryable<Menu>, IIncludableQueryable<Menu, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
         return await menuRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
    }

    public async Task<IPaginate<Menu?>?> GetListAsync(Expression<Func<Menu, bool>>? predicate = null, Func<IQueryable<Menu>, IOrderedQueryable<Menu>>? orderBy = null, Func<IQueryable<Menu>, IIncludableQueryable<Menu, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IPaginate<Menu> response = await menuRepository.GetListAsync(
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

    public async Task<Menu> UpdateAsync(Menu request)
    {
         return await menuRepository.UpdateAsync(request);
    }


}
