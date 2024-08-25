
using Core.Persistence.Repositories;
using QrMenu.Application.Repositories;
using QrMenu.Domain.Entities;
using QrMenu.Persistence.Contexts;

namespace QrMenu.Persistence.Repositories;

public class MenuQrCodeRepository : EfRepositoryBase<MenuQrCode,
    int, BaseDbContext>, IMenuQrCodeRepository
{
    public MenuQrCodeRepository(BaseDbContext context) 
        : base(context) { }
}
