
using FluentValidation;

namespace QrMenu.Application.Features.ItemImages.Commands.Create
{
    public class CreateItemImageValidator : AbstractValidator<CreateItemImageCommand>
    {
        public CreateItemImageValidator()
        {
        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Description is required.");
        }
    }
}
