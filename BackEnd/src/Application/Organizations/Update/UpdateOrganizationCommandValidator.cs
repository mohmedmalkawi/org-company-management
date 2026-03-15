using FluentValidation;

namespace Application.Organizations.Update;

internal sealed class UpdateOrganizationCommandValidator : AbstractValidator<UpdateOrganizationCommand>
{
    public UpdateOrganizationCommandValidator()
    {
        RuleFor(c => c.OrganizationId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Code).NotEmpty();
        RuleFor(c => c.Country).NotEmpty();
        RuleFor(c => c.Phone).NotEmpty();
        RuleFor(c => c.FullAddress).NotEmpty();
    }
}
