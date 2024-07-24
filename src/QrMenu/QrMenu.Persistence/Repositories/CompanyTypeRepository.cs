using Core.Persistence.Repositories;
using QrMenu.Application.Repositories;
using QrMenu.Domain.Entities;
using QrMenu.Persistence.Contexts;

namespace QrMenu.Persistence.Repositories;

public class CompanyTypeRepository : EfRepositoryBase<CompanyType,
    int, BaseDbContext>, ICompanyTypeRepository
{
    public CompanyTypeRepository(BaseDbContext context) 
        : base(context) { }
}
