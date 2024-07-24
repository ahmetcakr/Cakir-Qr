using Core.Persistence.Repositories;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Repositories;

public interface ICompanyRepository : IAsyncRepository<Company, int>, IRepository<Company, int> { }
