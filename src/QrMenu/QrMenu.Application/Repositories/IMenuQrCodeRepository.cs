
using Core.Persistence.Repositories;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Repositories;
    public interface IMenuQrCodeRepository :  
        IAsyncRepository<MenuQrCode, int>, IRepository<MenuQrCode, int> { }
