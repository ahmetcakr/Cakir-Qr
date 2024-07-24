using Core.Persistence.Repositories;
using QrMenu.Application.Repositories;
using QrMenu.Domain.Entities;
using QrMenu.Persistence.Contexts;

namespace QrMenu.Persistence.Repositories;

public class CompanyRepository : EfRepositoryBase<Company,
    int, BaseDbContext>, ICompanyRepository
{
    public CompanyRepository(BaseDbContext context) 
        : base(context) { }
}
