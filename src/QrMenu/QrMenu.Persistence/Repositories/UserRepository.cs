using Core.Persistence.Repositories;
using Core.Security.Entities;
using QrMenu.Application.Repositories;
using QrMenu.Persistence.Contexts;

namespace QrMenu.Persistence.Repositories;

public class UserRepository : EfRepositoryBase<User, int, BaseDbContext>, IUserRepository
{
    public UserRepository(BaseDbContext context)
        : base(context) { }
}
