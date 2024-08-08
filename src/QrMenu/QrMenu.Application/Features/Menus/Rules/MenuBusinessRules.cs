
using Core.CrossCuttingConcerns.Exceptions.Types;
using QrMenu.Application.Repositories;

namespace QrMenu.Application.Features.Menus.Rules
{
    public class MenuBusinessRules(IMenuRepository menuRepository)
    {
        public async Task MenuIdShouldBeExist(int id)
        {
            bool doesExist = await menuRepository.AnyAsync(predicate: e => e.Id == id, enableTracking: false);
            if (!doesExist)
                throw new BusinessException("Menu does not exist.");
        }
    }
}
