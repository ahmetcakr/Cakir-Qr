
using FluentValidation;

namespace QrMenu.Application.Features.Categories.Commands.Create
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
        RuleFor(e => e.CategoryName)
            .NotEmpty().WithMessage("CategoryName is required.");

        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Description is required.");
        }
    }
}
