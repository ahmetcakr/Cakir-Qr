using Core.Persistence.Repositories;
using Core.Security.Entities;
using QrMenu.Application.Repositories;
using QrMenu.Persistence.Contexts;

namespace QrMenu.Persistence.Repositories;

public class EmailAuthenticatorRepository : EfRepositoryBase<EmailAuthenticator, int, BaseDbContext>, IEmailAuthenticatorRepository
{
    public EmailAuthenticatorRepository(BaseDbContext context)
        : base(context) { }

    public async Task<EmailAuthenticator?> GetByUserIdAsync(int userId)
    {
        var emailAuth = await GetAsync(x => x.UserId == userId);

        return emailAuth;
    }
}
