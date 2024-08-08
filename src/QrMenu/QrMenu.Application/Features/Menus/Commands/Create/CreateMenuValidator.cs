
using FluentValidation;

namespace QrMenu.Application.Features.Menus.Commands.Create
{
    public class CreateMenuValidator : AbstractValidator<CreateMenuCommand>
    {
        public CreateMenuValidator()
        {
        RuleFor(e => e.MenuName)
            .NotEmpty().WithMessage("MenuName is required.");

        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Description is required.");
        }
    }
}
