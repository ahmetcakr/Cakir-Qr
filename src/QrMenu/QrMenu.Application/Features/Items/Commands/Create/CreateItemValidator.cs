
using FluentValidation;

namespace QrMenu.Application.Features.Items.Commands.Create
{
    public class CreateItemValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemValidator()
        {
        RuleFor(e => e.ItemName)
            .NotEmpty().WithMessage("ItemName is required.");

        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Description is required.");
        }
    }
}
