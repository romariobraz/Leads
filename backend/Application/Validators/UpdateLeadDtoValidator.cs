using FluentValidation;
using LeadQualifier.Application.DTOs.Leads;

namespace LeadQualifier.Application.Validators;

public class UpdateLeadDtoValidator : AbstractValidator<UpdateLeadDto>
{
    public UpdateLeadDtoValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => x.Email is not null)
            .WithMessage("Formato de email inválido.");

        RuleFor(x => x.Phone)
            .Matches(@"^\+?[0-9\s\-\(\)]+$")
            .When(x => x.Phone is not null)
            .WithMessage("Telefone inválido.");

        RuleFor(x => x.Budget)
            .GreaterThanOrEqualTo(0)
            .When(x => x.Budget.HasValue)
            .WithMessage("Budget não pode ser negativo.");

        RuleFor(x => x.Name)
            .MinimumLength(2)
            .When(x => x.Name is not null);

        RuleFor(x => x.Company)
            .NotEmpty()
            .When(x => x.Company is not null);
    }
}
