
using Core.CrossCuttingConcerns.Exceptions.Types;
using QrMenu.Application.Repositories;

namespace QrMenu.Application.Features.ItemImages.Rules
{
    public class ItemImageBusinessRules(IItemImageRepository ıtemImageRepository)
    {
        public async Task ItemImageIdShouldBeExist(int id)
        {
            bool doesExist = await ıtemImageRepository.AnyAsync(predicate: e => e.Id == id, enableTracking: false);
            if (!doesExist)
                throw new BusinessException("ItemImage does not exist.");
        }
    }
}
