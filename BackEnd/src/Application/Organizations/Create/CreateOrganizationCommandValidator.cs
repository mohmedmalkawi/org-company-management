using FluentValidation;

namespace Application.Organizations.Create;

internal sealed class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
{
    public CreateOrganizationCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Code).NotEmpty();
        RuleFor(c => c.Country).NotEmpty();
        RuleFor(c => c.Phone).NotEmpty();
        RuleFor(c => c.FullAddress).NotEmpty();
    }
}
