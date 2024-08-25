using Core.Helpers.QrHelper;
using MassTransit;
using QRCoder;
using QrMenu.Application.Features.MenuQrCodes.Commands.Create;
using QrMenu.Application.Features.MenuQrCodes.Commands.Update;
using QrMenu.Application.Features.Menus.Commands.Update;
using QrMenu.Application.Services.MenuQrCodesService;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Features.MenuQrCodes.Consumers;

public class CreatedMenuQrCodeResponseConsumer(
    IMenuQrCodeService menuQrCodeService) : IConsumer<CreatedMenuQrCodeResponse>
{
    public async Task Consume(ConsumeContext<CreatedMenuQrCodeResponse> context)
    {
         
        MenuQrCode updateMenu = new ()
         {
             Id = context.Message.Id,
             MenuId = context.Message.MenuId,
             QrCodeText = context.Message.QrCodeText,
             QrCode = await QrGenerator.GenerateQrCodeAsync(context.Message.QrCodeText)
         };

        await menuQrCodeService.UpdateAsync(updateMenu);
    }
}
