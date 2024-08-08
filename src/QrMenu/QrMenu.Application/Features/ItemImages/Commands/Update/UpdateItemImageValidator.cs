
using FluentValidation;

namespace QrMenu.Application.Features.ItemImages.Commands.Update
{
    public class UpdateItemImageValidator : AbstractValidator<UpdateItemImageCommand>
    {
        public UpdateItemImageValidator()
        {
        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Description is required.");
        }
    }
}
