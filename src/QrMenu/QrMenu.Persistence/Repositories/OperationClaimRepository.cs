using Core.Persistence.Repositories;
using Core.Security.Entities;
using QrMenu.Application.Repositories;
using QrMenu.Persistence.Contexts;

namespace QrMenu.Persistence.Repositories;

public class OperationClaimRepository : EfRepositoryBase<OperationClaim, int, BaseDbContext>, IOperationClaimRepository
{
    public OperationClaimRepository(BaseDbContext context)
        : base(context) { }
}
