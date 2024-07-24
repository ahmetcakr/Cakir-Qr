using FluentValidation;

namespace QrMenu.Application.Features.Companies.Commands.Create;

public class CreateCompanyValidator : AbstractValidator<CreateCompanyCommand>
{

    public CreateCompanyValidator()
    {
        RuleFor(c => c.CompanyName)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(c => c.CompanyTypeId).NotEqual(0)
            .WithMessage("{PropertyName} is required.");

        RuleFor(c => c.Address)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }

}
