
using FluentValidation;

namespace QrMenu.Application.Features.MenuQrCodes.Commands.Create
{
    public class CreateMenuQrCodeValidator : AbstractValidator<CreateMenuQrCodeCommand>
    {
        public CreateMenuQrCodeValidator()
        {
        RuleFor(e => e.QrCodeText)
            .NotEmpty().WithMessage("QrCodeText is required.");
        }
    }
}
