using FluentValidation;
using LeadQualifier.Application.DTOs.Leads;

namespace LeadQualifier.Application.Validators;

public class CreateLeadDtoValidator : AbstractValidator<CreateLeadDto>
{
    public CreateLeadDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MinimumLength(2);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório.")
            .EmailAddress().WithMessage("Formato de email inválido.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Telefone é obrigatório.")
            .Matches(@"^\+?[0-9\s\-\(\)]+$")
            .WithMessage("Telefone inválido.");

        RuleFor(x => x.Company)
            .NotEmpty().WithMessage("Empresa é obrigatória.");

        RuleFor(x => x.Budget)
            .GreaterThanOrEqualTo(0).WithMessage("Budget não pode ser negativo.");

        RuleFor(x => x.Need)
            .NotEmpty().WithMessage("Need é obrigatório.");

        RuleFor(x => x.Authority)
            .NotEmpty().WithMessage("Authority é obrigatório.");
    }
}
