using Core.Persistence.Repositories;
using Core.Security.Entities;
using QrMenu.Application.Repositories;
using QrMenu.Persistence.Contexts;

namespace QrMenu.Persistence.Repositories;

public class OtpAuthenticatorRepository : EfRepositoryBase<OtpAuthenticator, int, BaseDbContext>, IOtpAuthenticatorRepository
{
    public OtpAuthenticatorRepository(BaseDbContext context)
        : base(context) { }
}
