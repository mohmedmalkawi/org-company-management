using FluentValidation;

namespace Application.Companies.Update;

internal sealed class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyCommandValidator()
    {
        RuleFor(c => c.CompanyId).NotEmpty();
        RuleFor(c => c.OrganizationId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Code).NotEmpty();
        RuleFor(c => c.Country).NotEmpty();
        RuleFor(c => c.Phone).NotEmpty();
        RuleFor(c => c.FullAddress).NotEmpty();
    }
}
