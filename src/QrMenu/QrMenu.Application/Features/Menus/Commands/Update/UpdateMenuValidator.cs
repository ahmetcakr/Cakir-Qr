
using FluentValidation;

namespace QrMenu.Application.Features.Menus.Commands.Update
{
    public class UpdateMenuValidator : AbstractValidator<UpdateMenuCommand>
    {
        public UpdateMenuValidator()
        {
        RuleFor(e => e.MenuName)
            .NotEmpty().WithMessage("MenuName is required.");

        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Description is required.");
        }
    }
}
