using FluentValidation;

namespace QrMenu.Application.Features.CompanyTypes.Commands.Update;

public class UpdateCompanyTypeValidator : AbstractValidator<UpdateCompanyTypeCommand>
{
    public UpdateCompanyTypeValidator()
    {
        RuleFor(c => c.TypeName)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}
