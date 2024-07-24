using Core.Persistence.Paging;
using QrMenu.Application.Features.Companies.Rules;
using QrMenu.Application.Repositories;
using QrMenu.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace QrMenu.Application.Services.CompaniesService;

public class CompanyManager(
    ICompanyRepository companyRepository,
    CompanyBusinessRules companyBusinessRules) : ICompanyService
{
    public async Task<Company> AddAsync(Company company)
    {
        await companyBusinessRules.CompanyNameShouldNotExistsWhenInsert(company.CompanyName);

        return await companyRepository.AddAsync(company);
    }

    public async Task<Company> DeleteAsync(Company company, bool permanent = false)
    {
        return await companyRepository.DeleteAsync(company, permanent);
    }

    public async Task<Company?> GetAsync(Expression<Func<Company, bool>> predicate, Func<IQueryable<Company>, IIncludableQueryable<Company, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        var Companies = await companyRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return Companies;
    }

    public async Task<IPaginate<Company?>?> GetListAsync(Expression<Func<Company, bool>>? predicate = null, Func<IQueryable<Company>, IOrderedQueryable<Company>>? orderBy = null, Func<IQueryable<Company>, IIncludableQueryable<Company, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IPaginate<Company> companyList = await companyRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );

        return companyList;
    }

    public async Task<Company> UpdateAsync(Company request)
    {
        Company company = await companyRepository.UpdateAsync(request);
        return company;
    }
}
