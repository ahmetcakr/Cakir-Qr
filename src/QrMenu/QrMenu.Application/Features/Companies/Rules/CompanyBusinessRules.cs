using Core.CrossCuttingConcerns.Exceptions.Types;
using QrMenu.Application.Repositories;

namespace QrMenu.Application.Features.Companies.Rules;

public class CompanyBusinessRules(ICompanyRepository companyRepository)
{
    public async Task CompanyIdShouldBeExist(int id)
    {
        bool doesExist = await companyRepository.AnyAsync(predicate: c => c.Id == id, enableTracking: false);
        if (!doesExist)
            throw new BusinessException("Company does not exist.");
    }

    public async Task CompanyNameShouldNotExistsWhenInsert(string companyName)
    {
        bool doesExists = await companyRepository.AnyAsync(predicate: c => c.CompanyName == companyName, enableTracking: false);
        if (doesExists)
            throw new BusinessException("Company name already exists.");
    }
}
