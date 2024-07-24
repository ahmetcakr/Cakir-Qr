using Core.Persistence.Repositories;
using Core.Security.Entities;
using QrMenu.Application.Repositories;
using QrMenu.Persistence.Contexts;

namespace QrMenu.Persistence.Repositories;

public class RefreshTokenRepository : EfRepositoryBase<RefreshToken, int, BaseDbContext>, IRefreshTokenRepository
{
    public RefreshTokenRepository(BaseDbContext context)
        : base(context) { }
}
