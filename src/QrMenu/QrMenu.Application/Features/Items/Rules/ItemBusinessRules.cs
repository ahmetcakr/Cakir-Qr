
using Core.CrossCuttingConcerns.Exceptions.Types;
using QrMenu.Application.Repositories;

namespace QrMenu.Application.Features.Items.Rules
{
    public class ItemBusinessRules(IItemRepository ıtemRepository)
    {
        public async Task ItemIdShouldBeExist(int id)
        {
            bool doesExist = await ıtemRepository.AnyAsync(predicate: e => e.Id == id, enableTracking: false);
            if (!doesExist)
                throw new BusinessException("Item does not exist.");
        }
    }
}
