using Core.CrossCuttingConcerns.Exceptions.Types;
using QrMenu.Application.Repositories;

namespace QrMenu.Application.Features.CompanyTypes.Rules;

public class CompanyTypeBusinessRules(ICompanyTypeRepository companyRepository)
{
    public async Task CompanyTypeIdShouldBeExist(int id)
    {
        bool doesExist = await companyRepository.AnyAsync(predicate: c => c.Id == id, enableTracking: false);
        if (!doesExist)
            throw new BusinessException("Company does not exist.");
    }

    public async Task CompanyTypeNameShouldNotExistsWhenInsert(string companyTypeName)
    {
        bool doesExists = await companyRepository.AnyAsync(predicate: c => c.TypeName == companyTypeName, enableTracking: false);
        if (doesExists)
            throw new BusinessException("Company name already exists.");
    }
}
