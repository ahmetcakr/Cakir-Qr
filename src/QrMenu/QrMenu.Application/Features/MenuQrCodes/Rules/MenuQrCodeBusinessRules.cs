
using Core.CrossCuttingConcerns.Exceptions.Types;
using QrMenu.Application.Repositories;

namespace QrMenu.Application.Features.MenuQrCodes.Rules
{
    public class MenuQrCodeBusinessRules(IMenuQrCodeRepository menuQrCodeRepository)
    {
        public async Task MenuQrCodeIdShouldBeExist(int id)
        {
            bool doesExist = await menuQrCodeRepository.AnyAsync(predicate: e => e.Id == id, enableTracking: false);
            if (!doesExist)
                throw new BusinessException("MenuQrCode does not exist.");
        }
    }
}
