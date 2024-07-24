using Core.Persistence.Repositories;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Repositories;

public interface ICompanyTypeRepository : IAsyncRepository<CompanyType, int>, IRepository<CompanyType, int> { }
