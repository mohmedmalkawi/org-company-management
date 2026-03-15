using FluentValidation;

namespace Application.Companies.Create;

internal sealed class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(c => c.OrganizationId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Code).NotEmpty();
        RuleFor(c => c.Country).NotEmpty();
        RuleFor(c => c.Phone).NotEmpty();
        RuleFor(c => c.FullAddress).NotEmpty();
    }
}
