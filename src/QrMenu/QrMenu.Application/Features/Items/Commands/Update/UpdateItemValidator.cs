
using FluentValidation;

namespace QrMenu.Application.Features.Items.Commands.Update
{
    public class UpdateItemValidator : AbstractValidator<UpdateItemCommand>
    {
        public UpdateItemValidator()
        {
        RuleFor(e => e.ItemName)
            .NotEmpty().WithMessage("ItemName is required.");

        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Description is required.");
        }
    }
}
