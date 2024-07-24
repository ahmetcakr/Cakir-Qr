using FluentValidation;

namespace QrMenu.Application.Features.Companies.Commands.Update;

public class UpdateCompanyValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyValidator()
    {
        RuleFor(c => c.CompanyName)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(c => c.CompanyTypeId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(c => c.CompanyTypeId).Equal(0)
            .WithMessage("{PropertyName} is required.");

        RuleFor(c => c.Address)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}
