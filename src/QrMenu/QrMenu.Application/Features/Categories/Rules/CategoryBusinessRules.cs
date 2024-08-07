
using Core.CrossCuttingConcerns.Exceptions.Types;
using QrMenu.Application.Repositories;

namespace QrMenu.Application.Features.Categories.Rules
{
    public class CategoryBusinessRules(ICategoryRepository categoryRepository)
    {
        public async Task CategoryIdShouldBeExist(int id)
        {
            bool doesExist = await categoryRepository.AnyAsync(predicate: e => e.Id == id, enableTracking: false);
            if (!doesExist)
                throw new BusinessException("Category does not exist.");
        }
    }
}
