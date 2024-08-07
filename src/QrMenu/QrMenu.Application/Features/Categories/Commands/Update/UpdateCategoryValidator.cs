
using FluentValidation;

namespace QrMenu.Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator()
        {
        RuleFor(e => e.CategoryName)
            .NotEmpty().WithMessage("CategoryName is required.");

        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Description is required.");
        }
    }
}
