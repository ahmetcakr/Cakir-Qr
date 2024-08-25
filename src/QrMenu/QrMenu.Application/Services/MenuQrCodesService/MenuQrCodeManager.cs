
using Core.Persistence.Paging;
using QrMenu.Application.Features.MenuQrCodes.Rules;
using QrMenu.Application.Repositories;
using QrMenu.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;


namespace QrMenu.Application.Services.MenuQrCodesService;
    public class MenuQrCodeManager(
        IMenuQrCodeRepository menuqrcodeRepository,
        MenuQrCodeBusinessRules menuqrcodeBusinessRules) : IMenuQrCodeService
{
    public async Task<MenuQrCode> AddAsync(MenuQrCode menuqrcode)
    {
         // await menuqrcodeBusinessRules;
         // you can add your business rules here
    
        return await menuqrcodeRepository.AddAsync(menuqrcode);
    }

    public async Task<MenuQrCode> DeleteAsync(MenuQrCode menuqrcode, bool permanent = false)
    {
        return await menuqrcodeRepository.DeleteAsync(menuqrcode, permanent);
    }

    public async Task<MenuQrCode?> GetAsync(Expression<Func<MenuQrCode, bool>> predicate, Func<IQueryable<MenuQrCode>, IIncludableQueryable<MenuQrCode, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
         return await menuqrcodeRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
    }

    public async Task<IPaginate<MenuQrCode?>?> GetListAsync(Expression<Func<MenuQrCode, bool>>? predicate = null, Func<IQueryable<MenuQrCode>, IOrderedQueryable<MenuQrCode>>? orderBy = null, Func<IQueryable<MenuQrCode>, IIncludableQueryable<MenuQrCode, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IPaginate<MenuQrCode> response = await menuqrcodeRepository.GetListAsync(
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

    public async Task<MenuQrCode> UpdateAsync(MenuQrCode request)
    {
         return await menuqrcodeRepository.UpdateAsync(request);
    }


}
