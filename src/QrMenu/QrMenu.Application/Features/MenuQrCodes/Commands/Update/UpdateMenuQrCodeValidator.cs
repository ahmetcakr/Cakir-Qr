
using FluentValidation;

namespace QrMenu.Application.Features.MenuQrCodes.Commands.Update
{
    public class UpdateMenuQrCodeValidator : AbstractValidator<UpdateMenuQrCodeCommand>
    {
        public UpdateMenuQrCodeValidator()
        {
        RuleFor(e => e.QrCodeText)
            .NotEmpty().WithMessage("QrCodeText is required.");
        }
    }
}
